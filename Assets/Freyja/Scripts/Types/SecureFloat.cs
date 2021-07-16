using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using Freyja.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Freyja.Types
{
    public class SecureFloat
    {
        public float Value
        {
            get
            {
                var data = Freyja.ReadCall();
                var stats = Freyja.Convert(data);
                
                return (float) stats[_key];
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

        public SecureFloat(float value, string key)
        {
            _key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public static float operator +(SecureFloat a, SecureFloat b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value + b.Value;
        }

        public static float operator +(SecureFloat a, int b)
        {
            return a.Value + b;
        }

        public static float operator -(SecureFloat a, SecureFloat b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value - b.Value;
        }
        
        public static float operator -(SecureFloat a, int b)
        {
            return a.Value - b;
        }
        
        public static bool operator ==([NotNull]SecureFloat a, [NotNull]SecureFloat b)
        {
            if (a._key != b._key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value == b.Value;
        }
        
        public static bool operator !=([NotNull]SecureFloat a, [NotNull]SecureFloat b)
        {
            return !(a == b);
        }

        public static explicit operator SecureFloat(float value)
        {
            return new SecureFloat(value, null);
        }

        public static implicit operator float(SecureFloat value)
        {
            return value.Value;
        }
    }
}
