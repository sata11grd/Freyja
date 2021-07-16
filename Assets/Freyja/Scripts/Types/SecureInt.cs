using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
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
                
                return (int) stats[_key];
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
                    Debug.Log(stats[_key]);
                    Freyja.WriteCall(stats);
                }
            }
        }
        
        private string _key;

        public SecureInt(int value, string key)
        {
            _key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
