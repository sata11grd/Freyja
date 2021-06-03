using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(Dll.childAdd(-5, 9));
    }
}
