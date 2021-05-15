using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using UnityEngine;

namespace Freyja.Demo.Demo1
{
    public class DemoSceneManager : MonoBehaviour
    {
        public void Test()
        {
            Debug.Log(Dll.add(-5, 3));
            Debug.Log(Dll.test());
        }
    }
}
