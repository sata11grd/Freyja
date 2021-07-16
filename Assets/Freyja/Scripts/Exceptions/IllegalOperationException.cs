using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja.Exceptions
{
    public class IllegalOperationException : Exception
    {
        public IllegalOperationException()
        {
            // ignored
        }

        public IllegalOperationException(string message) : base(message)
        {
            // ignored
        }

        public IllegalOperationException(string message, Exception inner) : base(message, inner)
        {
            // ignored
        }

        protected IllegalOperationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            // ignored
        }
    }
}