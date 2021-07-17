using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Freyja.Core;
using Freyja.FileIO;
using Freyja.Meta;
using Freyja.Types;
using UnityEditor;
using UnityEditor.VersionControl;
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

            var result = new string[256, 256];
            result[0, 0] = "Secure Mode [sec]";
            result[0, 1] = "Non Secure Mode [sec]";
            
            var stopwatch = new Stopwatch();

            // secure mode test
            FreyjaSettings.Instance().NonSecureMode = false;

            stopwatch.Start();
            for (var i = 0; i < times; ++i)
            {
                Freyja.WriteCall(value);
            }
            stopwatch.Stop();
            
            result[1, 0] = stopwatch.Elapsed.ToString();

            // non secure mode test
            FreyjaSettings.Instance().NonSecureMode = true;
            
            stopwatch.Restart();
            for (var i = 0; i < times; ++i)
            {
                Freyja.WriteCall(value);
            }
            stopwatch.Stop();
            
            result[1, 1] = stopwatch.Elapsed.ToString();
            
            // out
            CsvFileManager.Write(result, Application.streamingAssetsPath + "/Freyja/Demo4 - Speed Test 2 Results/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
            
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
    }
}