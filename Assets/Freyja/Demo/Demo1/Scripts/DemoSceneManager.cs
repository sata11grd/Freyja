using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using Freyja.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Freyja.Demo.Demo1
{
    public class DemoSceneManager : MonoBehaviour
    {
        [SerializeField] private InputField nameInputField;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private Button writeCallButton;
        [SerializeField] private Button readCallButton;

        private const string FrdFilePath = "C:\\Users\\sfuna\\Desktop\\.frd";
        private const string LogFilePath = "C:\\Users\\sfuna\\Desktop\\frey.log";
        
        public void Test()
        {
            Freyja.WriteCall("test", FrdFilePath, "sv7F", LogFilePath);
            var value = Freyja.ReadCall(FrdFilePath, "sv7F", LogFilePath);
            Debug.Log(value);
        }

        private void Awake()
        {
            Debug.Log(FreyjaSettings.Instance().DecryptionKey);
            writeCallButton.onClick.AddListener(() =>
            {
                Freyja.WriteCall("test", FrdFilePath, "sv7F", LogFilePath);
            });
            
            readCallButton.onClick.AddListener(() =>
            {
                var value = Freyja.ReadCall(FrdFilePath, "sv7F", LogFilePath);
                Debug.Log(value);
            });
        }
    }
}
