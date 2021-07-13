using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        /*
        [DllImport("App", CallingConvention = CallingConvention.StdCall)]
        public static extern int add(int a, int b);

        [DllImport("App", CallingConvention = CallingConvention.Cdecl)]
        public static extern int test();
        */
        
        [DllImport("App", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string frey_read_call(string frd_fpath, string decryption_key, string log_fpath);
        
        [DllImport("App", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern void frey_write_call(string data, string frd_fpath, string encryption_key, string log_fpath);
    }
}
