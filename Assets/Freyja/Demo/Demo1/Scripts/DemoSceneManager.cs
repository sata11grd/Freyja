using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using UnityEngine;

namespace Freyja.Demo.Demo1
{
    public class DemoSceneManager : MonoBehaviour
    {
        private void Start()
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            Debug.Log(Dll.add(-5, 3));
            Debug.Log(Dll.sub(-5, 3));
            Debug.Log(Dll.mul(-5, 3));
            Debug.Log(Dll.initialize_enclave());
        }
    }
}
