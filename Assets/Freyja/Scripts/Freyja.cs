using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Freyja.Core;
using UnityEditor;
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

        /// <summary>
        /// Write the data from dictionary format.
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public static void WriteCall(Dictionary<string, (Type, object)> data)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var item in data)
            {
                stringBuilder.Append("[");
                stringBuilder.Append(item.Key);
                stringBuilder.Append("]");
                stringBuilder.Append(item.Value.Item2);
                stringBuilder.Append(":");

                if (item.Value.Item1 == typeof(int))
                {
                    stringBuilder.Append(typeof(int));
                }
                else if (item.Value.Item1 == typeof(float))
                {
                    stringBuilder.Append(typeof(float));
                }
                else if (item.Value.Item1 == typeof(string))
                {
                    stringBuilder.Append(typeof(string));
                }
                else
                {
                    throw new Exception("The given type is not supported in current version of Freyja.");
                }
            }

            Debug.Log(stringBuilder.ToString());
            WriteCall(stringBuilder.ToString());
        }
    }
}