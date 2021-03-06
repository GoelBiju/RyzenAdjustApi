﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static RyzenAdjustApi.Definitions;


namespace RyzenAdjustApi
{
    public unsafe class SMU
    {
        // TODO: smu_t:
        //          - nb (type: nb_t - uint32_t - int)
        //          - msg (type: u32 - uint32_t - int)
        //          - rep (type: u32 - uint32_t - int)
        //          - arg_base (type: u32 - uint32_t - int)
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct smu_t
        {
            public uint nb;

            public uint msg;

            public uint rep;

            public uint arg_base;
        }

        //
        public const int AMD_VENDOR_ID = 0x1022;
        public const int NB_DEVICE_ID = 0x15d0;

        //
        public const int NB_PCI_REG_ADDR_ADDR = 0xB8;
        public const int NB_PCI_REG_DATA_ADDR = 0xBC;

        //
        public static uint C2PMSG_ARGx_ADDR(uint y, int x) => (uint)(y + 4 * x);

        //
        public enum SMU_TYPE
        {
            TYPE_MP1,
            TYPE_PSMU,
            TYPE_COUNT
        }

        //
        public const int MP1_C2PMSG_MESSAGE_ADDR = 0x3B10528;
        public const int MP1_C2PMSG_RESPONSE_ADDR = 0x3B10564;
        public const int MP1_C2PMSG_ARG_BASE = 0x3B10998;

        //
        public const int PSMU_C2PMSG_MESSAGE_ADDR = 0x3B10a20;
        public const int PSMU_C2PMSG_RESPONSE_ADDR = 0x3B10a80;
        public const int PSMU_C2PMSG_ARG_BASE = 0x3B10a88;

        //
        //public const int REP_MSG_OK = 0x1;
        //public const int REP_MSG_FAILED = 0xFF;
        //public const int REP_MSG_UNKNOWN_CMD = 0xFE;
        //public const int REP_MSG_CMD_REJECTED_PREREQ = 0xFD;
        //public const int REP_MSG_CMD_REJECTED_BUSY = 0xFC;

        //
        public const int SMU_TEST_MSG = 0x1;

        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct smu_service_args_t
        {
            public uint arg0;

            public uint arg1;

            public uint arg2;

            public uint arg3;

            public uint arg4;

            public uint arg5;
        }


        // TODO: Custom response object (?)
        public class smu_response
        {
            public uint response;

            public smu_service_args_t args;
        }

        private WinRing0 hwOps;

        public SMU()
        {
            hwOps = new WinRing0();
            Console.WriteLine("Initialised WinRing0 object for general use.");
        }


        // TODO: Convenience methods for accessing WinRing0.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Initialise()
        {
            return hwOps.Initialise();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool InitialisePCIObj()
        {
            return hwOps.init_pci_obj();
        }
        // void free_pci_obj(pci_obj_t obj);

        public int GetNB(bool obj)
        {
            return hwOps.get_nb(obj);
        }
        // void free_nb(nb_t nb);


        // SMU related methods.

        // smu_t get_smu(nb_t nb, int smu_type);
        public smu_t GetSMU(uint nb, SMU_TYPE smu_type)
        {
            // Create new SMU structure.
            smu_t smu = new smu_t();

            // Test message response and arguments.
            //uint rep;
            smu_service_args_t arg = new smu_service_args_t();

            // Fill the SMU information.
            smu.nb = nb;

            switch (smu_type)
            {
                case SMU_TYPE.TYPE_MP1:
                    smu.msg = MP1_C2PMSG_MESSAGE_ADDR;
                    smu.rep = MP1_C2PMSG_RESPONSE_ADDR;
                    smu.arg_base = MP1_C2PMSG_ARG_BASE;
                    break;

                case SMU_TYPE.TYPE_PSMU:
                    smu.msg = PSMU_C2PMSG_MESSAGE_ADDR;
                    smu.rep = PSMU_C2PMSG_RESPONSE_ADDR;
                    smu.arg_base = PSMU_C2PMSG_ARG_BASE;
                    break;

                default:
                    Console.WriteLine("Failed to get SMU, unknown SMU_TYPE: " + smu_type);
                    break;
            }

            // Send a test message.
            SMUResponseCode resp = (SMUResponseCode)SMUServiceReq(smu, Messages.TestMessage, arg).response;
            if (resp != SMUResponseCode.Result_OK)
                Console.WriteLine("Failed to get SMU: {0}, test message REP: {1}", smu_type, resp);

            Console.WriteLine("SMU_TYPE: {0}, Test message response: {1}", smu_type, resp);
            return smu;
        }


        // u32 smu_service_req(smu_t smu, u32 id, smu_service_args_t *args);
        public smu_response SMUServiceReq(smu_t smu, Messages id, smu_service_args_t args)
        {
            smu_response serviceResponse = new smu_response();
            uint response = 0x0;
            //smu_service_args_t args = *inArgs;

            // Debug information.
            Console.WriteLine("SMU_SERVICE REQ_ID: 0x{0:X}", id);
            Console.WriteLine("SMU_SERVICE REQ: arg0: 0x{0:X}, arg1: 0x{1:X}, arg2: 0x{2:X}, " +
                "arg3: 0x{3:X}, arg4: 0x{4:X}, arg5: 0x{5:X}",
                args.arg0, args.arg1, args.arg2, args.arg3, args.arg4, args.arg5);


            // Clear the response.
            hwOps.smn_reg_write(smu.nb, smu.rep, 0x0);

            // Pass arguments.
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 0), args.arg0);
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 1), args.arg1);
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 2), args.arg2);
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 3), args.arg3);
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 4), args.arg4);
            hwOps.smn_reg_write(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 5), args.arg5);

            // Send message ID.
            hwOps.smn_reg_write(smu.nb, smu.msg, (uint)id);

            // Wait until response has changed.
            //while (response == 0x0)
            //{
            //    response = hwOps.smn_reg_read(smu.nb, smu.rep);
            //}
            do
            {
                response = hwOps.smn_reg_read(smu.nb, smu.rep);
            } while (response == 0x0);

            // Read back arguments.
            args.arg0 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 0));
            args.arg1 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 1));
            args.arg2 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 2));
            args.arg3 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 3));
            args.arg4 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 4));
            args.arg5 = hwOps.smn_reg_read(smu.nb, C2PMSG_ARGx_ADDR(smu.arg_base, 5));

            // Response.
            Console.WriteLine("SMU_SERVICE REP: REP: 0x{0:X}, arg0: 0x{1:X}, arg1: 0x{2:X}, arg2: 0x{3:X}, " +
                "arg3: 0x{4:X}, arg4: 0x{5:X}, arg5: 0x{6:X}",
                response, args.arg0, args.arg1, args.arg2, args.arg3, args.arg4, args.arg5);

            Console.WriteLine(args.arg0);

            // Create smu_response object.
            serviceResponse.response = response;
            serviceResponse.args = args;

            //return response;
            return serviceResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Deinitialise()
        {
            hwOps.Deinitialise();
        }
    }
}
