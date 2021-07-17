using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Freyja.Core;
using Freyja.Meta;
using Freyja.Types;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Freyja.Demo.Demo4
{
    public class DemoSceneManager : MonoBehaviour
    {
        [SerializeField] private Button runTestButton;

        private void Awake()
        {
            runTestButton.onClick.AddListener(OnRunTestButtonClicked);
        }

        private void OnRunTestButtonClicked()
        {
            const int times = 10;
            const string value = "TestCaseString";
            
            var stopwatch = new Stopwatch();

            // secure mode test
            FreyjaSettings.Instance().NonSecureMode = false;

            stopwatch.Start();
            for (var i = 0; i < times; ++i)
            {
                Freyja.WriteCall(value);
            }
            
            Debug.Log(stopwatch.Elapsed);

            // non secure mode test
            FreyjaSettings.Instance().NonSecureMode = true;
            
            stopwatch.Restart();
            for (var i = 0; i < times; ++i)
            {
                Freyja.WriteCall(value);
            }
            
            Debug.Log(stopwatch.Elapsed);
        }
    }
}