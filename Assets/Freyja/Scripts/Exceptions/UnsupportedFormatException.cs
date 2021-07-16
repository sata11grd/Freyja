using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja.Exceptions
{
    public class UnsupportedFormatException : Exception
    {
        public UnsupportedFormatException()
        {
            // ignored
        }

        public UnsupportedFormatException(string message) : base(message)
        {
            // ignored
        }

        public UnsupportedFormatException(string message, Exception inner) : base(message, inner)
        {
            // ignored
        }

        protected UnsupportedFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            // ignored
        }
    }
}