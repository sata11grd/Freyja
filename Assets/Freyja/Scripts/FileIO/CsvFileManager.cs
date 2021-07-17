using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Freyja.FileIO
{
    public static class CsvFileManager
    {
        public static void Write(string[,] value, string path)
        {
            var sb = new StringBuilder();
            
            for (var row = 0; row < value.GetLength(0); ++row)
            {
                for (var col = 0; col < value.GetLength(1); ++col)
                {
                    sb.Append(value[row, col]);
                    sb.Append(',');
                }

                sb.Append('\n');
            }
            
            FileManager.Write(sb.ToString(), path);
        }

        public static string[,] Read(string path)
        {
            var read = FileManager.Read(path);
            var lines = read.Split('\n');
            var matrix = new List<string[]>();            

            foreach (var line in lines)
            {
                matrix.Add(line.Split(','));
            }

            var value = new string[matrix.Count, matrix[0].Length];

            for (var row = 0; row < value.GetLength(0); ++row)
            {
                for (var col = 0; col < value.GetLength(1); ++col)
                {
                    value[row, col] = matrix[row][col];
                }
            }

            return value;
        }
    }
}