﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static RyzenAdjustApi.Definitions;
using static RyzenAdjustApi.SMU;


namespace RyzenAdjustApi
{
    // TODO: Ryzen Access object:
    //          - nb (type: nb_t - uint32_t - int)
    //          - pci_obj (type: pci_obj - bool)
    //          - mp1_smu (type: smu_t)
    //          - psmu (type: smu_t)
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ryzen_access
    {
        public bool use;

        public uint nb;

        public bool pci_obj;

        public smu_t mp1_smu;

        public smu_t psmu;
    }

    public unsafe class AdjustApi
    {
        //private WinRing0 hw;
        private SMU accessSmu;

        private bool initialised;

        public AdjustApi()
        {
            // Get an instance of WinRing0.
            //hw = new WinRing0();
            accessSmu = new SMU();
            Console.WriteLine("Initialised SMU object for general use.");

            // Initialise the WinRing0 for use.
            initialised = accessSmu.Initialise();
        }


        // Class Finalizer to call the cleanup method.
        ~AdjustApi()
        {
            Cleanup();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ryzen_access GetRyzenAccess()
        {
            // 
            ryzen_access ry = new ryzen_access();
            ry.use = true;

            if (!initialised)
            {
                ry.use = false;
                Console.WriteLine("RyzenApi not initialised.");
                return ry;
            }

            //
            smu_service_args_t args = new smu_service_args_t();

            // If the ryzen_access object is unusable,
            // then the pci_obj will be set to false.
            ry.pci_obj = accessSmu.InitialisePCIObj();
            if (!ry.pci_obj)
            {
                ry.use = false;
                Console.WriteLine("Unable to get PCI Obj.");
                //return ry;
            }

            // GetNB will always return the same pci address (0x0)?.
            ry.nb = (uint)accessSmu.GetNB(ry.pci_obj);
            if (ry.nb != 0x0)
            {
                ry.use = false;
                Console.WriteLine("Unable to get NB Obj.");
                //return ry;
            }

            // TODO: smu_t will never be null, so check contents before continuing.
            //       Set ry.mp1_smu.nb to 1 by default, GetSMU will provide new SMU set to 0.
            //       If the get failed then nb will still be set to 1.
            ry.mp1_smu.nb = 1;
            ry.mp1_smu = accessSmu.GetSMU(ry.nb, SMU_TYPE.TYPE_MP1);
            if (ry.mp1_smu.nb == 1)
            {
                ry.use = false;
                Console.WriteLine("MP1_SMU nb still set to 1.");
                //return ry;
            }
            Console.WriteLine("PSMU nb set to: " + ry.mp1_smu.nb);

            // Similar process with psmu.
            ry.psmu.nb = 1;
            ry.psmu = accessSmu.GetSMU(ry.nb, SMU_TYPE.TYPE_PSMU);
            if (ry.psmu.nb == 1)
            {
                ry.use = false;
                Console.WriteLine("PSMU nb still set to 1.");
                //return ry;
            }
            Console.WriteLine("PSMU nb set to: " + ry.psmu.nb);

            // Check if the device is a Ryzen nb SMU.
            args = accessSmu.SMUServiceReq(ry.mp1_smu, Definitions.Messages.GetBiosIfVersion, args).args;
            Console.WriteLine(args.arg0);
            if (args.arg0 < 0x5)
            {
                ry.use = false;
                Console.WriteLine("Not a Ryzen NB SMU, BIOS Interface Version: 0x{0}", args.arg0);
            }
            // Return the ryzen access object.
            Console.WriteLine("Initialised and returning ryzen_access object.");
            return ry;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ServiceRequest(ryzen_access ry, Messages id, int value)
        {
            // TODO: Ensure that the ryzen access object is suitable for use
            // (check all properties of the ryzen_access struct).
            if (initialised)
            {
                smu_service_args_t args = new smu_service_args_t();
                args.arg0 = (uint)value;

                smu_response smuResp = accessSmu.SMUServiceReq(ry.mp1_smu, id, args);
                if (smuResp.response == 0x01)
                {
                    Console.WriteLine("Successful SMU request - Response: {0}", smuResp.response);
                    return true;
                }
                Console.WriteLine("Failed SMU request - Response: {0}", smuResp.response);
                return false;
            }
            Console.WriteLine("RyzenApi not initialised.");
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStapmLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSustainedPowerLimit, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetFastLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetFastPPTLimit, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetSlowLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSlowPPTLimit, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetSlowTime(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSlowPPTTimeConstant, value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ry"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStapmTime(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetStapmTimeConstant, value);
        }


        public bool SetTemperatureLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetTctlMax, value);
        }


        public bool SetVrmCurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetVrmCurrentLimit, value);
        }


        public bool SetVrmSoCCurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetVrmSocCurrentLimit, value);
        }

        public bool SetVrmMaxCurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetVrmMaximumCurrentLimit, value);
        }

        public bool SetVrmSoCMaxCurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetVrmSocMaximumCurrentLimit, value);
        }

        public bool SetPsi0CurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetPSI0CurrentLimit, value);
        }

        public bool SetPsi0SoCCurrentLimit(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetPSI0SocCurrentLimit, value);
        }

        public bool SetMaxGfxClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxGfxClk, value);
        }

        public bool SetMinGfxClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMinGfxClk, value);
        }

        public bool SetMaxSoCClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxSocclkByFreq, value);
        }

        public bool SetMinSoCClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMinSocclkByFreq, value);
        }

        public bool SetMaxFClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxFclkByFreq, value);
        }

        public bool SetMinFClkFreq(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMinFclkByFreq, value);
        }

        public bool SetMaxVcn(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxVcn, value);
        }

        public bool SetMinVcn(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMinVcn, value);
        }

        public bool SetMaxLClk(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxLclk, value);
        }

        public bool SetMinLClk(ryzen_access ry, int value)
        {
            return ServiceRequest(ry, Messages.SetSoftMaxLclk, value);
        }


        public void Cleanup()
        {
            accessSmu.Deinitialise();
        }
    }
}
