using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        [DllImport("App", CallingConvention = CallingConvention.StdCall)]
        public static extern int add(int a, int b);
    }
}
