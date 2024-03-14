using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MinGun
{
    public enum CarType
    {
        front,
        rear,
        all
    }
    [RequireComponent(typeof(CarManager)), RequireComponent(typeof(WheelAnim)), RequireComponent(typeof(InputManager))]
    public class CarControl : MonoBehaviour
    {
        private CarManager carManager;
        private InputManager inputManager;
        private Rigidbody rigidbody;
        public float KPH;
        
        private List<WheelCollider> wheelColliders;
        
        public CarType carType;
        public Vector3 centerOfMess;


        public float downForceValue = 50f;
        public float motorTorque;
        public float turnWheelRot;

        private void Start()
        {
            carManager = this.GetComponent<CarManager>();
            inputManager = this.GetComponent<InputManager>();
            rigidbody = this.GetComponent<Rigidbody>();
            wheelColliders = carManager.wheelColliders;

            carManager.rigidbody.centerOfMass = centerOfMess;
        }

        private void FixedUpdate()
        {
            WheelRotate();
            CarMove();
            AddDownForce();
        }

        private void CarMove()
        {
            float finalPower = 0;


            finalPower = motorTorque;
            RunCar(finalPower);
        }

        private void RunCar(float power)
        {
            int fr = 0, ba = 0;

            switch (carType)
            {
                case CarType.front:
                    fr = 0;
                    ba = 2;
                    break;
                
                case CarType.rear:
                    fr = 2;
                    ba = 4;
                    break;
                
                case CarType.all:
                    fr = 0;
                    ba = 4;
                    break;
            }
            for (int i = fr; i < ba; i++)
            {
                wheelColliders[i].motorTorque = power * inputManager.vertical / (ba-fr);
            }

            KPH = rigidbody.velocity.magnitude * 3.6f;
        }
  
        private void WheelRotate()
        {
            float hori = inputManager.horizontal;
            wheelColliders[hori < 0 ? 0 : 1].steerAngle = hori * Mathf.Rad2Deg * Mathf.Atan(2.55f / (turnWheelRot - 0.75f));
            wheelColliders[hori < 0 ? 1 : 0].steerAngle = hori * Mathf.Rad2Deg * Mathf.Atan(2.55f / (turnWheelRot + 0.75f));
        }

        private void AddDownForce()
        {
            rigidbody.AddForce(-transform.up * downForceValue * rigidbody.velocity.magnitude);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + centerOfMess, 0.1f);
        }
    }
}
