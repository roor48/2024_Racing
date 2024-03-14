using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CarManager : MonoBehaviour
    {
        public CarControl carControl;
        public Rigidbody rigidbody;

        private Transform wheelCollidersContainer;
        public List<WheelCollider> wheelColliders;
        private Transform wheelMeshesContainer;
        public List<Transform> wheelMeshes;

        private void Awake()
        {
            carControl = this.GetComponent<CarControl>();
            rigidbody = this.GetComponent<Rigidbody>();
            
            wheelCollidersContainer = transform.Find("WheelCols");
            wheelMeshesContainer = transform.Find("WheelMeshes");

            for (int i = 0; i < wheelCollidersContainer.childCount; i++)
            {
                wheelColliders.Add(wheelCollidersContainer.GetChild(i).GetComponent<WheelCollider>());
            }

            for (int i = 0; i < wheelMeshesContainer.childCount; i++)
            {
                wheelMeshes.Add(wheelMeshesContainer.GetChild(i));
            }
        }
    }
}
