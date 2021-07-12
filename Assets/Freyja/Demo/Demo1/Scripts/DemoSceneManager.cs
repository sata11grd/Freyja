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
            Debug.Log(Dll.frey_write_call_test("input data for test."));
        }
    }
}
