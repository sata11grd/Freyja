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
            var parameterTable = new Dictionary<string, (Type, object)>();
            
            parameterTable.Add("user_id", (typeof(string), userIdInputField.text));
            parameterTable.Add("hp", (typeof(float), hpSlider.value));
            parameterTable.Add("mp", (typeof(float), mpSlider.value));
            
            Freyja.WriteCall(parameterTable);
        }

        private void SecureLoad()
        {
            var read = Freyja.ReadCall();
            var converted = Freyja.Convert(read);

            var userId = (string) converted["user_id"];
            var hp = (float) converted["hp"];
            var mp = (float) converted["mp"];

            userIdInputField.text = userId;
            hpSlider.value = hp;
            mpSlider.value = mp;
        }

        private void Awake()
        {
            secureSaveButton.onClick.AddListener(SecureSave);
            secureLoadButton.onClick.AddListener(SecureLoad);
        }
    }
}
