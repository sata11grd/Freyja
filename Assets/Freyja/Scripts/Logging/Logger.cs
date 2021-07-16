using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Freyja.Logging
{
    public static class Logger
    {
        public static void Log(string value)
        {
            Debug.Log("[Freyja] " + value);
        }
    }
}
