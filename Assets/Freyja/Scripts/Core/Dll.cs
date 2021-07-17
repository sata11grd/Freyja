using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Freyja.Core
{
    public static class Dll
    {
        /// <summary>
        /// Write the data by Enclave.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frd_fpath"></param>
        /// <param name="encryption_key"></param>
        /// <param name="log_fpath"></param>
        [DllImport("FreyApp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern void frey_write_call(string data, string frd_fpath, string encryption_key, string log_fpath, bool non_secure_mode);
        
        /// <summary>
        /// Read the data by Enclave.
        /// </summary>
        /// <param name="frd_fpath"></param>
        /// <param name="decryption_key"></param>
        /// <param name="log_fpath"></param>
        /// <returns></returns>
        [DllImport("FreyApp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string frey_read_call(string frd_fpath, string decryption_key, string log_fpath, bool non_secure_mode);
    }
}
