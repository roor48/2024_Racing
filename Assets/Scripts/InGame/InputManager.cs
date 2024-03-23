using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public enum Driver
    {
        Enemy,
        Player,
    }
    public class InputManager : MonoBehaviour
    {
        private LapSystem lapSystem;
            
        public Driver driverType;
        public float horizontal;
        public float vertical;
        public bool handbreak;

        [Range(0, 5)] public float steerForce;

        private void Start()
        {
            lapSystem = this.GetComponent<LapSystem>();
        }

        private void Update()
        {
            switch (driverType)
            {
                case Driver.Enemy:
                    OnAI();
                    break;
                
                case Driver.Player:
                    OnKeyBoard();
                    break;
            }
        }
        private void OnKeyBoard()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            handbreak = Input.GetAxis("Jump") != 0;
        }

        private void OnAI()
        {
            Vector3 relative = transform.InverseTransformPoint(lapSystem.currentWaypoint.transform.position);
            relative /= relative.magnitude;

            horizontal = relative.x / relative.magnitude * steerForce;
        }
    }
}
