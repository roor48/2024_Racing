using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class ChooseManager : MonoBehaviour
    {
        public Transform rotObject;
        public float rotSpeed;
        private void FixedUpdate()
        {
            rotObject.Rotate(Time.fixedDeltaTime * rotSpeed * Vector3.up);
        }
    }
}
