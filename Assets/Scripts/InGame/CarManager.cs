using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CarManager : MonoBehaviour
    {
        [HideInInspector] public CarControl carControl;
        [HideInInspector] public Rigidbody rigidbody;
        private InputManager inputManager;

        private Transform wheelCollidersContainer;
        public List<WheelCollider> wheelColliders;
        private Transform wheelMeshesContainer;
        public List<Transform> wheelMeshes;

        private List<Transform> nodes;

        private void Awake()
        {
            carControl = this.GetComponent<CarControl>();
            rigidbody = this.GetComponent<Rigidbody>();
            inputManager = this.GetComponent<InputManager>();
            nodes = GameObject.FindWithTag("Path").GetComponent<TrackWayPoint>().nodes;
            
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

        private void Update()
        {
            
        }
    }
}
