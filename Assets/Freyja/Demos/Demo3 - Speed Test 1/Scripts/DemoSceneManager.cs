using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Freyja.Core;
using Freyja.Types;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Freyja.Demo.Demo3
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
            // preparation
            const int cycleStepCount = 5;
            const int cycleCount = 10;
            const int input = 100;
            
            var result = new List<TimeSpan>();
            var stopwatch = new Stopwatch();
            
            var y = new SecureInt(input);
            
            stopwatch.Start();

            for (var i = 0; i < 10; i++)
            {
                y.Value = input;
            }
            
            Debug.Log(stopwatch.Elapsed);
            return;

            var buf = stopwatch.Elapsed;

            int x = 0;
            stopwatch.Restart();
            while (stopwatch.Elapsed < buf)
            {
                x = x + 1;
            }
            Debug.Log(stopwatch.Elapsed);
            Debug.Log(x);
            return;
        }
    }
}