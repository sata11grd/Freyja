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
            Debug.Log("Hi");
            Debug.Log(Dll.add_function(1, 1));
            Debug.Log(Dll.add_function(2, 5));
            Debug.Log(Dll.add_function(-7, 2));
        }
    }
}
