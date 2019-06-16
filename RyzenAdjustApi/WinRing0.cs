using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenLibSys;

namespace RyzenAdjustApi
{
    class WinRing0
    {
        private Ols ols;

        private bool initialisedOls = false;

        private bool nb_pci_obj = true;

        private int nb_pci_address = 0x0;

        public WinRing0()
        {
            ols = new Ols();
            //Initialise();
        }

        public bool Initialise()
        {
            // Initialise the DLL 
            int retCode = ols.InitializeOls();
            initialisedOls = (retCode == 1) ? true : false;
            if (initialisedOls)
            {
                Console.WriteLine("Initialised Ols object for WinRing0.");
                return true;
            }
            Console.WriteLine("Failed to initialise Ols, ols.InitialiseOls() returned: {0}", retCode);
            return false;
        }


        // pci_obj_t init_pci_obj();
        // pci_obj_t - bool
        public bool init_pci_obj()
        {
            // Initialise the Ols object.
            //ols.InitializeOls();

            if (initialisedOls)
            {
                // Check the DLL status.
                if (ols.GetDllStatus() == 0)
                {
                    Console.WriteLine("WinRing0 initialised for ryzen_access.");
                    return nb_pci_obj;
                }
                Console.WriteLine("Error initialising WinRing0, DLL Status code:", ols.GetDllStatus());
                return false;
            }
            Console.WriteLine("Ols not initialised.");
            return false;
        }

        // nb_t get_nb(pci_obj_t obj);
        // nb_t - uint
        public int get_nb(bool obj)
        {
            Console.WriteLine("Returning nb: " + nb_pci_address);
            return nb_pci_address;
        }


        // uint smn_reg_read(nb_t nb, u32 addr);
        public uint smn_reg_read(uint nb, uint addr)
        {
            if (initialisedOls)
            {
                ols.WritePciConfigDword(nb, SMU.NB_PCI_REG_ADDR_ADDR, (uint)(addr & (~0x3)));
                return ols.ReadPciConfigDword(nb, SMU.NB_PCI_REG_DATA_ADDR);
            }
            Console.WriteLine("Ols not initialised.");
            return 0;
        }

        // void smn_reg_write(nb_t nb, u32 addr, u32 data);
        public void smn_reg_write(uint nb, uint addr, uint data)
        {
            if (initialisedOls)
            {
                ols.WritePciConfigDword(nb, SMU.NB_PCI_REG_ADDR_ADDR, addr);
                ols.WritePciConfigDword(nb, SMU.NB_PCI_REG_DATA_ADDR, data);
            } else
            {
                Console.WriteLine("Ols not initialised.");
            }
        }

        // void free_pci_obj(pci_obj_t obj);
        public void Deinitialise()
        {
            if (initialisedOls)
            {
                ols.DeinitializeOls();
                Console.WriteLine("Deinitialised Ols.");
            }
        }
    }
}
