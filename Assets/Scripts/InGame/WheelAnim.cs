using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class WheelAnim : MonoBehaviour
    {
        private CarManager carManager;

        private void Start()
        {
            carManager = this.GetComponent<CarManager>();
        }

        private void Update()
        {
            for (int i = 0; i < carManager.wheelMeshes.Count; i++)
            {
                Vector3 pos;
                Quaternion quat;
                carManager.wheelColliders[i].GetWorldPose(out pos, out quat);
                carManager.wheelMeshes[i].position = pos;
                carManager.wheelMeshes[i].rotation = quat;
            }
        }
    }
}
