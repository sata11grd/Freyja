using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        [DllImport("add", CallingConvention = CallingConvention.StdCall)]
        public static extern int add_function(int a, int b);
    }
}
