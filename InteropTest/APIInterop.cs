using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InteropTest
{
    internal unsafe class APIInterop
    {
        //private void* ry;

        //[StructLayout(LayoutKind.Sequential)]
        //public unsafe struct smu_t
        //{
        //    public uint* nb;

        //    public uint msg;

        //    public uint rep;

        //    public uint arg_base;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public unsafe struct ryzen_access
        //{
        //    public uint* nb;

        //    public bool* pci_obj;

        //    public smu_t mp1_smu;

        //    public smu_t psmu;
        //}

        [DllImport("libryzenadj.dll")]
        public static extern void* init_ryzenadj();

        [DllImport("libryzenadj.dll")]
        public static extern int set_stapm_limit(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_fast_limit(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_slow_limit(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_slow_time(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_stapm_time(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_tctl_temp(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_vrm_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_vrmsoc_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_vrmmax_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_vrmsocmax_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_psi0_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_psi0soc_current(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_max_gfxclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_min_gfxclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_max_socclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_min_socclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_max_fclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_min_fclk_freq(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_max_vcn(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_min_vcn(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_max_lclk(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int set_min_lclk(void* ry, uint value);

        [DllImport("libryzenadj.dll")]
        public static extern int send_request(uint requestCode, uint value);

        // NOTE: Displays blank C++ runtime window upon starting when using cleanup_ryzenadj()
        //       Possibly due to passing in ryzen_access object/pointer. (FIXED?)
        // Just pass void* access object.
        [DllImport("libryzenadj.dll")]
        public static extern void cleanup_ryzenadj(void* ry);

        [DllImport("libryzenadj.dll")]
        public static extern int add(uint x, uint y);


        public APIInterop()
        {
            // NOTE: We cannot instantiate the an access object (in a function/method) 
            //       and leave it unmanaged (causes a heap corruption?). 
            //       Make sure to use cleanup_ryzenadj at the end of the function (otherwise make it a class variable).

            void* ry;

            ry = init_ryzenadj();

            set_stapm_limit(ry, 15000);

            // Cleanup ryzen access object.
            //cleanup_ryzenadj(ry);

            //int result = send_request(26, 15000);
            //Console.WriteLine(result);
            //Console.WriteLine(res);
            //Console.ReadLine();

            //int result = add(12, 12);

            //Console.WriteLine(add(1, 2));
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetStapmLimit(int value)
        //{
        //    void* ry = init_ryzenadj();

        //    int result = set_stapm_limit(ry, (uint)value);
        //    cleanup_ryzenadj(ry);

        //    if (result == 0)
        //        return true;
        //    return false;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetFastLimit(int value)
        //{
        //    int result = set_fast_limit(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetSlowLimit(int value)
        //{
        //    int result = set_slow_limit(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetStapmTime(int value)
        //{
        //    int result = set_stapm_time(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetTemperatureLimit(int value)
        //{
        //    int result = set_tctl_temp(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetVRMCurrent(int value)
        //{
        //    int result = set_vrm_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxVRMCurrent(int value)
        //{
        //    int result = set_vrmmax_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetSoCVRMCurrent(int value)
        //{
        //    int result = set_vrmsoc_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxSocVRMCurrent(int value)
        //{
        //    int result = set_vrmsocmax_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetPSI0Current(int value)
        //{
        //    int result = set_psi0_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetSoCPSI0Current(int value)
        //{
        //    int result = set_psi0soc_current(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxGfxClkFreq(int value)
        //{
        //    int result = set_max_gfxclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMinGfxClkFreq(int value)
        //{
        //    int result = set_max_gfxclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        //public bool SetMaxSoCClkFreq(int value)
        //{
        //    int result = set_max_socclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMinSoCClkFreq(int value)
        //{
        //    int result = set_min_socclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxFClkFreq(int value)
        //{
        //    int result = set_max_fclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMinFClkFreq(int value)
        //{
        //    int result = set_min_fclk_freq(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxVCN(int value)
        //{
        //    int result = set_max_vcn(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMinVCN(int value)
        //{
        //    int result = set_min_vcn(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMaxLClk(int value)
        //{
        //    int result = set_max_lclk(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// NOTE: Experimental
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetMinLClk(int value)
        //{
        //    int result = set_min_lclk(ryzen_access, (uint)value);
        //    if (result == 0)
        //        return true;
        //    return false;
        //}

        /// <summary>
        /// 
        /// </summary>
        //public void Cleanup()
        //{
        //    cleanup_ryzenadj(ryzen_access);
        //}
    }
}
