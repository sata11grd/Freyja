using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Freyja.Demo.Demo1
{
    [CustomEditor(typeof(DemoSceneManager))]
    public class ExampleScriptEditor : Editor {
        public override void OnInspectorGUI(){
            base.OnInspectorGUI ();

            var demoSceneManager = target as DemoSceneManager;

            if (GUILayout.Button("Test")){
                demoSceneManager.Test();
            }
        }
    } 
}