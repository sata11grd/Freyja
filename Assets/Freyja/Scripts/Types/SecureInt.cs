using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Freyja.Types
{
    public readonly struct SecureInt
    {
        private readonly int _value;

        public SecureInt(int value)
        {
            _value = value;
        }

        public static implicit operator SecureInt(int value)
        {
            //Dll.frey_write_call_test(value.ToString(), "");
            return new SecureInt(value);
        }

        public static implicit operator int(SecureInt value)
        {
            //Dll.frey_read_call_test("");
            return value._value;
        }

        public static int operator +(SecureInt a, SecureInt b)
        {
            return a._value + b._value;
        }

        public static int operator -(SecureInt a, SecureInt b)
        {
            return a._value - b._value;
        }

        public static bool operator ==(SecureInt a, SecureInt b)
        {
            return a._value == b._value;
        }

        public static bool operator !=(SecureInt a, SecureInt b)
        {
            return a._value == b._value;
        }
        
        public bool Equals(SecureInt other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is SecureInt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value;
        }
    }
}
