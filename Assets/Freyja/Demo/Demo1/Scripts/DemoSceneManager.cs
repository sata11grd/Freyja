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
        
        public void Test()
        {
            SecureInt tp = 15;
            Debug.Log((int)tp);

            var a = tp + 5;
            Debug.Log(a);

            return;
            Debug.Log(Dll.frey_write_call_test("sample text here", "C:\\Users\\sfuna\\Desktop\\freyja"));

            SecureInt hp;
            hp = 5;

            var mp = new SecureInt(3);
            if (mp < 3)
            {
                
            }
            
        }

        private void Awake()
        {
            writeCallButton.onClick.AddListener(() =>
            {
                Freyja.WriteCall("test");
            });
            
            readCallButton.onClick.AddListener(() =>
            {
                Freyja.ReadCall();
            });
        }
    }
}
