using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class RotCamera : MonoBehaviour
    {
        public float rotSpeed = 3f;
        private void Update()
        {
            transform.Rotate(Time.deltaTime * rotSpeed * Vector3.up);
        }
    }
}
