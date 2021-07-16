using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja.Exceptions
{
    public class UndefinedOperationException : Exception
    {
        public UndefinedOperationException()
        {
            // ignored
        }

        public UndefinedOperationException(string message) : base(message)
        {
            // ignored
        }

        public UndefinedOperationException(string message, Exception inner) : base(message, inner)
        {
            // ignored
        }

        protected UndefinedOperationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            // ignored
        }
    }
}