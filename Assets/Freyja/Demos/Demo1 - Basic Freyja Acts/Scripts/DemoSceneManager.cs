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
        [SerializeField] private InputField userIdInputField;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private Button secureSaveButton;
        [SerializeField] private Button secureLoadButton;

        private void SecureSave()
        {
            Debug.Log("[Freyja] SecureSave is called.");
            
            var parameterTable = new Dictionary<string, (Type, object)>();
            parameterTable.Add("user_id", (typeof(string), userIdInputField.text));
            parameterTable.Add("hp", (typeof(float), hpSlider.value));
            parameterTable.Add("mp", (typeof(float), mpSlider.value));
            
            Freyja.WriteCall(parameterTable);
        }

        private void SecureLoad()
        {
            Debug.Log("[Freyja] SecureLoad is called.");

            var read = Freyja.ReadCall();
            
            Debug.Log("[Freyja] loaded value: \n" + read);

            var converted = Freyja.Convert(read);

            var userId = (string) converted["user_id"];
            var hp = (float) converted["hp"];
            var mp = (float) converted["mp"];
            
            Debug.Log(userId);
            Debug.Log(hp);
            Debug.Log(mp);
        }

        private void Awake()
        {
            secureSaveButton.onClick.AddListener(SecureSave);
            secureLoadButton.onClick.AddListener(SecureLoad);
        }

        private void Update()
        {
            // if input field null then do not save it
        }
    }
}
