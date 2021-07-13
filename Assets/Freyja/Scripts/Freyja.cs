using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using UnityEngine;

namespace Freyja
{
    public static class Freyja
    {
        public static void WriteCall(string data, string frdFilePath, string encryptionKey = null, string logFilePath = null)
        {
            Dll.frey_write_call(data, frdFilePath, encryptionKey, logFilePath);
        }

        public static string ReadCall(string frdFilePath, string encryptionKey = null, string logFilePath = null)
        {
            return Dll.frey_read_call(frdFilePath, encryptionKey, logFilePath);
        }
    }
}