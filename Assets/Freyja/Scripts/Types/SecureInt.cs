using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using Freyja.Exceptions;
using JetBrains.Annotations;
using UnityEngine;

namespace Freyja.Types
{
    public class SecureInt
    {
        public int Value
        {
            get
            {
                var data = Freyja.ReadCall();
                var stats = Freyja.Convert(data);

                return (int) stats[Key];
            }
            set
            {
                var data = Freyja.ReadCall();
                
                if (string.IsNullOrEmpty(data))
                {
                    var stats = new Dictionary<string, object>();
                    stats.Add(Key, value);
                    Freyja.WriteCall(stats);
                }
                else
                {
                    var stats = Freyja.Convert(data);
                    stats[Key] = value;
                    Freyja.WriteCall(stats);
                }
            }
        }

        private string Key => GetHashCode().ToString();

        public SecureInt(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public static int operator +(SecureInt a, SecureInt b)
        {
            if (a.Key != b.Key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value + b.Value;
        }

        public static int operator +(SecureInt a, int b)
        {
            return a.Value + b;
        }

        public static int operator -(SecureInt a, SecureInt b)
        {
            if (a.Key != b.Key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value - b.Value;
        }
        
        public static int operator -(SecureInt a, int b)
        {
            return a.Value - b;
        }
        
        public static bool operator ==([NotNull]SecureInt a, [NotNull]SecureInt b)
        {
            if (a.Key != b.Key)
            {
                throw new IllegalOperationException("The given key identifiers are not be matched.");
            }

            return a.Value == b.Value;
        }
        
        public static bool operator !=([NotNull]SecureInt a, [NotNull]SecureInt b)
        {
            return !(a == b);
        }

        public static explicit operator SecureInt(int value)
        {
            return new SecureInt(value);
        }

        public static implicit operator int(SecureInt value)
        {
            return value.Value;
        }
    }
}
