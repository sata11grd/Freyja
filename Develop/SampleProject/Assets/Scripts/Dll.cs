using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class Dll
{
    [DllImport("ChildDll", CallingConvention = CallingConvention.StdCall)]
    public static extern int childAdd(int a, int b);
}
