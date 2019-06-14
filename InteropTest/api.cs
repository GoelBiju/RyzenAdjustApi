using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static InteropTest.SMU;

namespace InteropTest
{
    // TODO: Ryzen Access object:
    //          - nb (type: nb_t - uint32_t - int)
    //          - pci_obj (type: pci_obj - bool)
    //          - mp1_smu (type: smu_t)
    //          - psmu (type: smu_t)
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ryzen_access
    {
        public uint nb;

        public bool pci_obj;

        public smu_t mp1_smu;

        public smu_t psmu;
    }

    unsafe class api
    {
        //private WinRing0 hw;
        private SMU accessSmu;

        public api()
        {
            // Get an instance of WinRing0.
            //hw = new WinRing0();
            accessSmu = new SMU();
            Console.WriteLine("Initialised SMU object for general use.");
        }

        public ryzen_access InitialiseRyzenAdj()
        {
            //
            smu_service_args_t args = new smu_service_args_t
            {
                arg0 = 0,
                arg1 = 0,
                arg2 = 0,
                arg3 = 0,
                arg4 = 0,
                arg5 = 0
            };

            // 
            ryzen_access ry = new ryzen_access();

            // If the ryzen_access object is unusable,
            // then the pci_obj will be set to false.
            ry.pci_obj = accessSmu.InitialisePCIObj();
            if (!ry.pci_obj)
            {
                Console.WriteLine("Unable to get PCI Obj.");
                return ry;
            }

            // GetNB will always return the same pci address (0x0)?.
            ry.nb = (uint)accessSmu.GetNB(ry.pci_obj);
            if (ry.nb != 0x0)
            {
                Console.WriteLine("Unable to get NB Obj.");
                return ry;
            }

            //
            //ry.mp1_smu = get_smu(ry.nb, SMU_TYPE.TYPE_MP1);



            Console.WriteLine("Initialised and returning ryzen_access object.");
            // Return the ryzen access object.
            return ry;
        }
    }
}
