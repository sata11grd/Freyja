using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja.Demo.Level
{
    public class Rotater : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;

        private void FixedUpdate()
        {
            transform.eulerAngles += speed;
        }
    }
}
