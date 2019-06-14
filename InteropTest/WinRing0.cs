using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenLibSys;

namespace InteropTest
{
    class WinRing0
    {
        private Ols ols;

        private bool nb_pci_obj = true;

        private int nb_pci_address = 0x0;

        public WinRing0()
        {
            ols = new Ols();
            Console.WriteLine("Initialised Ols object for WinRing0.");
        }


        // pci_obj_t init_pci_obj();
        // pci_obj_t - bool
        public bool init_pci_obj()
        {
            // Initialise the Ols object.
            ols.InitializeOls();

            // Check the DLL status.
            if (ols.GetDllStatus() == 0)
            {
                Console.WriteLine("WinRing0 initialised for ryzen_access.");
                return nb_pci_obj;
            }
            Console.WriteLine("Error initialising WinRing0, DLL Status code:", ols.GetDllStatus());
            return false;
        }

        // nb_t get_nb(pci_obj_t obj);
        // nb_t - uint
        public int get_nb(bool obj)
        {
            Console.WriteLine("Returning nb: ", nb_pci_address);
            return nb_pci_address;
        }
    }
}
