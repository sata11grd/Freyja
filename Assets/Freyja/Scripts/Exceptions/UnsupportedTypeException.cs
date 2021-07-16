using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja.Exceptions
{
    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException()
        {
            // ignored
        }

        public UnsupportedTypeException(string message) : base(message)
        {
            // ignored
        }

        public UnsupportedTypeException(string message, Exception inner) : base(message, inner)
        {
            // ignored
        }

        protected UnsupportedTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            // ignored
        }
    }
}