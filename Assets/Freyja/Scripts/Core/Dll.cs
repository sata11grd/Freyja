using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        [DllImport("Freyja", CallingConvention = CallingConvention.StdCall)]
        public static extern int add(int a, int b);

        [DllImport("Freyja", CallingConvention = CallingConvention.StdCall)]
        public static extern int sub(int a, int b);
    }
}
