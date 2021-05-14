using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        [DllImport("freyja", CallingConvention = CallingConvention.StdCall)]
        public static extern int add(int a, int b);
        
        [DllImport("freyja", CallingConvention = CallingConvention.StdCall)]
        public static extern int sub(int a, int b);
        
        [DllImport("freyja", CallingConvention = CallingConvention.StdCall)]
        public static extern int mul(int a, int b);
    }
}
