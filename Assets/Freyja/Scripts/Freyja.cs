using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

                stringBuilder.Append(",");
            }

            WriteCall(stringBuilder.ToString());
        }

        /// <summary>
        /// Convert the given value from string to parameter table.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, (Type, object)> Convert(string source)
        {
            Debug.Log(source);

            var lines = source.Split(',');

            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; ++i)
                {
                    if (line[i] == ']')
                    {
                        var key = line.Substring(1, i - 1);

                        for (var j = i; j < line.Length; ++j)
                        {
                            if (line[j] == ':')
                            {
                                var value = line.Substring(i, j - 1);
                                var typeOfString = line.Substring(j, line.Length - 1);
                                
                                Debug.Log(key);
                                Debug.Log(value);
                                Debug.Log(typeOfString);
                            }
                        }
                    }
                }
            }
            
            return null;
        }
    }
}