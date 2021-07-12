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
        public static extern string frey_read_call_test();
    }
}
