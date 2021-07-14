using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using UnityEngine;

namespace Freyja
{
    public static class Freyja
    {
        /// <summary>
        /// Write the data by Enclave.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frdFilePath"></param>
        /// <param name="encryptionKey"></param>
        /// <param name="logFilePath"></param>
        public static void WriteCall(string data, string frdFilePath = null, string encryptionKey = null, string logFilePath = null)
        {
            if (frdFilePath is null)
            {
                frdFilePath = FreyjaSettings.Instance().FreyjaDataFilePath;
            }
            
            if (encryptionKey is null)
            {
                encryptionKey = FreyjaSettings.Instance().EncryptionKey;
            }
            
            if (logFilePath is null)
            {
                logFilePath = FreyjaSettings.Instance().LogFilePath;
            }
            
            Dll.frey_write_call(data, frdFilePath, encryptionKey, logFilePath);
        }

        /// <summary>
        /// Read the data by Enclave.
        /// </summary>
        /// <param name="frdFilePath"></param>
        /// <param name="encryptionKey"></param>
        /// <param name="logFilePath"></param>
        /// <returns></returns>
        public static string ReadCall(string frdFilePath = null, string encryptionKey = null, string logFilePath = null)
        {
            if (frdFilePath is null)
            {
                frdFilePath = FreyjaSettings.Instance().FreyjaDataFilePath;
            }
            
            if (encryptionKey is null)
            {
                encryptionKey = FreyjaSettings.Instance().EncryptionKey;
            }
            
            if (logFilePath is null)
            {
                logFilePath = FreyjaSettings.Instance().LogFilePath;
            }
            
            return Dll.frey_read_call(frdFilePath, encryptionKey, logFilePath);
        }
    }
}