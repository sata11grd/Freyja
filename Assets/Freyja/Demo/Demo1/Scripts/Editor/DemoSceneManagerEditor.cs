using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Freyja.Demo.Demo1.Editor
{
    [CustomEditor(typeof(DemoSceneManager))]
    public class ExampleScriptEditor : UnityEditor.Editor {
        public override void OnInspectorGUI(){
            base.OnInspectorGUI ();

            var demoSceneManager = target as DemoSceneManager;

            if (GUILayout.Button("Test")){
                demoSceneManager.Test();
            }
        }
    } 
}