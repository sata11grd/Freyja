using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Freyja.Core;
using Freyja.Exceptions;
using Freyja.Meta;
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
            
            Dll.frey_write_call(data, frdFilePath, encryptionKey, logFilePath, FreyjaSettings.Instance().NonSecureMode);
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
            
            return Dll.frey_read_call(frdFilePath, encryptionKey, logFilePath, FreyjaSettings.Instance().NonSecureMode);
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
                    throw new UnsupportedTypeException("The given type is not supported in current version of Freyja.");
                }

                stringBuilder.Append(",");
            }

            WriteCall(stringBuilder.ToString().TrimEnd(','));
        }

        /// <summary>
        /// Write the data from dictionary format.
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public static void WriteCall(Dictionary<string, object> data)
        {
            var table = new Dictionary<string, (Type, object)>();
            
            foreach (var item in data)
            {
                if (item.Value is int)
                {
                    table.Add(item.Key, (typeof(int), item.Value));
                }
                else if (item.Value is float)
                {
                    table.Add(item.Key, (typeof(float), item.Value));
                }
                else if (item.Value is string)
                {
                    table.Add(item.Key, (typeof(string), item.Value));
                }
                else
                {
                    throw new UnsupportedTypeException("The given type is not supported in current version of Freyja.");
                }
            }
            
            WriteCall(table);
        }

        /// <summary>
        /// Convert the given value from string to parameter table.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Convert(string source)
        {
            var table = new Dictionary<string, object>();
            var lines = source.Split(',');
            
            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; ++i)
                {
                    if (line[i] == ']')
                    {
                        var key = line.Substring(1, i - 1);
                        var valueType = line.Substring(i + 1, line.Length - i - 1).Split(':').ToArray();
                        var value = valueType[0];
                        var type = Type.GetType(valueType[1]);

                        if (type == typeof(int))
                        {
                            table.Add(key, int.Parse(value));
                        }
                        else if (type == typeof(float))
                        {
                            table.Add(key, float.Parse(value));
                        }
                        else if (type == typeof(string))
                        {
                            table.Add(key, value);
                        }
                        else
                        {
                            throw new UnsupportedTypeException($"The given type is not supported in current version of Freyja.");
                        }
                    }
                }
            }
            
            return table;
        }
    }
}