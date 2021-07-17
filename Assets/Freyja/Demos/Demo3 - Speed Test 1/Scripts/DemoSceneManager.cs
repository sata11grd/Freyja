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

            // int speed
            int x;
            
            stopwatch.Start();
            
            for (var i = 0; i < cycleCount; ++i)
            {
                for (var j = 0; j < cycleStepCount; ++j)
                {
                    x = input;
                }
                    
                result.Add(stopwatch.Elapsed);
            }
            
            // SecureInt speed
            var y = new SecureInt(input, "hp");

            stopwatch.Restart();
            
            for (var i = 0; i < cycleCount; ++i)
            {
                for (var j = 0; j < cycleStepCount; ++j)
                {
                    y.Value = input;
                }
                    
                result.Add(stopwatch.Elapsed);
            }

            // show result
            result.ForEach(time => Debug.Log(time));
        }
    }
}