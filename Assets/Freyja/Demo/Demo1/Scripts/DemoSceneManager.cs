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
            Freyja.WriteCall("[name]jhondoe:System.String[hp]100:System.Int32");
            Debug.Log(Freyja.ReadCall());
            return;
            var ptable = new Dictionary<string, (Type, object)>();
            ptable.Add("name", (typeof(string), "johndoe"));
            ptable.Add("hp", (typeof(int), 100));
            ptable.Add("mp", (typeof(int), 50));
            ptable.Add("spd", (typeof(float), 1.21f));
            Freyja.WriteCall(ptable);

            Debug.Log(Freyja.ReadCall());
            
            return;
            Freyja.WriteCall("test", FrdFilePath, "sv7F", LogFilePath);
            var value = Freyja.ReadCall(FrdFilePath, "sv7F", LogFilePath);
            Debug.Log(value);
        }

        private void Awake()
        {
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
