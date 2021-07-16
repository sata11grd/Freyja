using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using Freyja.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Freyja.Types
{
    public class SecureString
    {
        public string Value
        {
            get
            {
                var data = Freyja.ReadCall();
                var stats = Freyja.Convert(data);
                
                return (string) stats[_key];
            }
            set
            {
                var data = Freyja.ReadCall();

                if (string.IsNullOrEmpty(data))
                {
                    var stats = new Dictionary<string, object>();
                    stats.Add(_key, value);
                    Freyja.WriteCall(stats);
                }
                else
                {
                    var stats = Freyja.Convert(data);
                    stats[_key] = value;
                    Freyja.WriteCall(stats);
                }
            }
        }
        
        private readonly string _key;

        public SecureString(string value, string key)
        {
            _key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
        
        public static string operator +(SecureString a, SecureString b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value + b.Value;
        }

        public static string operator +(SecureString a, int b)
        {
            return a.Value + b;
        }

        public static string operator -(SecureString a, SecureString b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            throw new UndefinedOperationException("Requested operation is undefined.");
        }
        
        public static string operator -(SecureString a, int b)
        {
            throw new UndefinedOperationException("Requested operation is undefined.");
        }
        
        public static bool operator ==([NotNull]SecureString a, [NotNull]SecureString b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value == b.Value;
        }
        
        public static bool operator !=([NotNull]SecureString a, [NotNull]SecureString b)
        {
            return !(a == b);
        }

        public static explicit operator SecureString(string value)
        {
            return new SecureString(value, null);
        }

        public static implicit operator string(SecureString value)
        {
            return value.Value;
        }
    }
}
