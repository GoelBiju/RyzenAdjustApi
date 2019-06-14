using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InteropTest
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
        public static int C2PMSG_ARGx_ADDR(uint y, int x) => (int)(y + 4 * x);

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
        public const int REP_MSG_OK = 0x1;
        public const int REP_MSG_FAILED = 0xFF;
        public const int REP_MSG_UNKNOWN_CMD = 0xFE;
        public const int REP_MSG_CMD_REJECTED_PREREQ = 0xFD;
        public const int REP_MSG_CMD_REJECTED_BUSY = 0xFC;

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

        private WinRing0 hwOps;

        public SMU()
        {
            hwOps = new WinRing0();
            Console.WriteLine("Initialised WinRing0 object for general use.");
        }

        // TODO: Convenience methods for accessing WinRing0.

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


        //public smu_t GetSMU(uint nb, int smu_type)
        //{
        //    // Create new SMU structure.
        //    smu_t smu = new smu_t();

        //    // Test message response and arguments.
        //    uint rep;
        //    smu_service_args_t arg = new smu_service_args_t
        //}


        // SMU related methods.

        // u32 smn_reg_read(nb_t nb, u32 addr);

        // void smn_reg_write(nb_t nb, u32 addr, u32 data);

        // smu_t get_smu(nb_t nb, int smu_type);

        // void free_smu(smu_t smu);

        // u32 smu_service_req(smu_t smu, u32 id, smu_service_args_t *args);
    }
}
