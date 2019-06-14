using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenLibSys;

namespace InteropTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ryzen_access access;

            // Get a new ryzenadj api object.
            api ryzenApi = new api();

            // Get a ryzen_access object to use.
            access = ryzenApi.InitialiseRyzenAdj();

            // Show ryzen_access properties.
            Console.WriteLine("ryzen_access.pci_obj: " + access.pci_obj);
            Console.WriteLine("ryzen_access.nb: " + access.nb);
            Console.WriteLine("ryzen_access.mp1_smu: " + access.mp1_smu);
            Console.WriteLine("ryzen_access.psmu: " + access.psmu);

            Console.ReadLine();

            //APIInterop api = new APIInterop();
            //uint num = 0x01;
            //Console.WriteLine(num + 2);
            //Console.ReadLine();

            // Initialise OLS.
            //Ols ols = new Ols();
            //ols.InitializeOls();

            // Check support library sutatus
            //switch (ols.GetStatus())
            //{
            //    case (uint)Ols.Status.NO_ERROR:
            //        Console.WriteLine("Ols status no error.");
            //        break;
            //    case (uint)Ols.Status.DLL_NOT_FOUND:
            //        Console.WriteLine("Status Error!! DLL_NOT_FOUND");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.Status.DLL_INCORRECT_VERSION:
            //        Console.WriteLine("Status Error!! DLL_INCORRECT_VERSION");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
            //        Console.WriteLine("Status Error!! DLL_INITIALIZE_ERROR");
            //        Environment.Exit(0);
            //        break;
            //}

            // Check WinRing0 status
            //Console.WriteLine(ols.GetDllStatus());
            //switch (ols.GetDllStatus())
            //{
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
            //        Console.WriteLine("Dll status no error.");
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
            //        Console.WriteLine("DLL Status Error!! OLS_DRIVER_NOT_LOADED");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
            //        Console.WriteLine("DLL Status Error!! OLS_UNSUPPORTED_PLATFORM");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
            //        Console.WriteLine("DLL Status Error!! OLS_DLL_DRIVER_NOT_FOUND");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
            //        Console.WriteLine("DLL Status Error!! OLS_DLL_DRIVER_UNLOADED");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
            //        Console.WriteLine("DLL Status Error!! DRIVER_NOT_LOADED_ON_NETWORK");
            //        Environment.Exit(0);
            //        break;
            //    case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
            //        Console.WriteLine("DLL Status Error!! OLS_DLL_UNKNOWN_ERROR");
            //        Environment.Exit(0);
            //        break;
            //}

            //String str = "";
            ////-----------------------------------------------------------------------------
            //// DLL Information
            ////-----------------------------------------------------------------------------
            //byte major = 0, minor = 0, revision = 0, release = 0;

            //str += "[DLL Version]\r\n";
            //ols.GetDllVersion(ref major, ref minor, ref revision, ref release);
            //str += major.ToString()
            //    + "." + minor.ToString()
            //    + "." + revision.ToString()
            //    + "." + release.ToString()
            //    + "\r\n";

            //str += "[Device Driver Version]\r\n";
            //ols.GetDriverVersion(ref major, ref minor, ref revision, ref release);
            //str += major.ToString()
            //    + "." + minor.ToString()
            //    + "." + revision.ToString()
            //    + "." + release.ToString()
            //    + "\r\n";

            //str += "[Device Driver Type]\r\n";
            //switch (ols.GetDriverType())
            //{
            //    case (uint)Ols.OlsDriverType.OLS_DRIVER_TYPE_WIN_9X:
            //        str += "OLS_DRIVER_TYPE_WIN_9X\r\n";
            //        break;
            //    case (uint)Ols.OlsDriverType.OLS_DRIVER_TYPE_WIN_NT:
            //        str += "OLS_DRIVER_TYPE_WIN_NT\r\n";
            //        break;
            //    case (uint)Ols.OlsDriverType.OLS_DRIVER_TYPE_WIN_NT_X64:
            //        str += "OLS_DRIVER_TYPE_WIN_NT_X64\r\n";
            //        break;
            //    case (uint)Ols.OlsDriverType.OLS_DRIVER_TYPE_WIN_NT_IA64:
            //        str += "OLS_DRIVER_TYPE_WIN_NT_IA64\r\n";
            //        break;
            //    default:
            //        str += "OLS_DRIVER_TYPE_UNKNOWN\r\n";
            //        break;
            //}

            ////-----------------------------------------------------------------------------
            //// TSC
            ////-----------------------------------------------------------------------------
            //uint index = 0, eax = 0, ebx = 0, ecx = 0, edx = 0;

            //str += "[TSC]\r\n";
            //if (ols.RdtscPx(ref eax, ref edx, (UIntPtr)1) != 0)
            //{
            //    str += "index     63-32    31-0\r\n";
            //    str += index.ToString("X8") + ": " + edx.ToString("X8")
            //        + " " + eax.ToString("X8") + "\r\n";
            //}
            //else
            //{
            //    str += "Failure : Change Process Affinity Mask\r\n";
            //}

            ////-----------------------------------------------------------------------------
            //// MSR
            ////-----------------------------------------------------------------------------
            //str += "[MSR]\r\n";
            //index = 0x00000010; // Time Stamp Counter
            //if (ols.RdmsrTx(index, ref eax, ref edx, (UIntPtr)1) != 0)
            //{
            //    str += "index     63-32    31-0\r\n";
            //    str += index.ToString("X8") + ": " + edx.ToString("X8")
            //        + " " + eax.ToString("X8") + "\r\n";
            //}
            //else
            //{
            //    str += "Failure : Change Thread Affinity Mask\r\n";
            //}

            ////-----------------------------------------------------------------------------
            //// CPUID (Standard/Extended)
            ////-----------------------------------------------------------------------------
            //uint maxCpuid = 0, maxCpuidEx = 0;

            //str += "[CPUID]\r\n";
            //str += "index     EAX      EBX      ECX      EDX  \r\n";

            //// Standard
            //ols.CpuidPx(0x00000000, ref maxCpuid, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            //for (index = 0x00000000; index <= maxCpuid; index++)
            //{
            //    ols.CpuidPx(index, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            //    str += index.ToString("X8") + ": "
            //        + eax.ToString("X8") + " " + ebx.ToString("X8") + " "
            //        + ecx.ToString("X8") + " " + edx.ToString("X8") + "\r\n";
            //}
            //// Extended
            //ols.CpuidPx(0x80000000, ref maxCpuidEx, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            //for (index = 0x80000000; index <= maxCpuidEx; index++)
            //{
            //    ols.CpuidPx(index, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            //    str += index.ToString("X8") + ": "
            //        + eax.ToString("X8") + " " + ebx.ToString("X8") + " "
            //        + ecx.ToString("X8") + " " + edx.ToString("X8") + "\r\n";
            //}

            ////-----------------------------------------------------------------------------
            //// PCI
            ////-----------------------------------------------------------------------------
            //uint address, value;

            //str += "[PCI]\r\n";

            //// All Device
            //str += "Bus Dev Fnc VendorDevice\r\n";
            //for (uint bus = 0; bus <= 128; bus++)
            //{
            //    for (uint dev = 0; dev < 32; dev++)
            //    {
            //        for (uint func = 0; func < 8; func++)
            //        {
            //            address = ols.PciBusDevFunc(bus, dev, func);
            //            value = ols.ReadPciConfigDword(address, 0x00);
            //            if ((value & 0xFFFF) != 0xFFFF && (value & 0xFFFF) != 0x0000)
            //            {
            //                str += ols.PciGetBus(address).ToString("X2") + "h "
            //                    + ols.PciGetDev(address).ToString("X2") + "h "
            //                    + ols.PciGetFunc(address).ToString("X2") + "h "
            //                    + ((uint)(value & 0x0000FFFF)).ToString("X04") + "h "
            //                    + ((uint)((value >> 16) & 0x0000FFFF)).ToString("X04") + "h\r\n";
            //            }
            //        }
            //    }
            //}

            //// Host Bridge
            //address = ols.FindPciDeviceByClass(0x06, 0x00, 0x00, 0);
            //if (address != 0xFFFFFFFF)
            //{
            //    str += "[PCI Confguration Space Dump] HostBridge\r\n";
            //    str += "    00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F\r\n";
            //    str += "---------------------------------------------------\r\n";

            //    for (int i = 0; i < 256; i += 16)
            //    {
            //        str += i.ToString("X2") + "|";
            //        for (int j = 0; j < 16; j++)
            //        {
            //            str += " " + (ols.ReadPciConfigByte(address, (byte)(i + j))).ToString("X2");
            //        }
            //        str += "\r\n";
            //    }
            //}

            //Console.WriteLine(str);
            //Console.ReadLine();
        }
    }
}
