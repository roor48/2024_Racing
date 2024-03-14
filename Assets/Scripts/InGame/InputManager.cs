using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class InputManager : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public bool handbreak;

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            handbreak = Input.GetAxis("Jump") != 0;
        }
    }
}
