using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Freyja.FileIO
{
    public static class FileManager
    {
        public static void Write(string value, string path)
        {
            using var writer = new StreamWriter(path);
            writer.Write(value);
        }

        public static string Read(string path)
        {
            using var reader = new StreamReader(path);

            return reader.ReadToEnd();
        }
    }
}
