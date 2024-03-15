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
        private GameManager gameManager;
        [HideInInspector] public InputManager inputManager;
        private Rigidbody rigidbody;
        
        private List<WheelCollider> wheelColliders;
        
        public CarType carType;
        public Vector3 centerOfMess;

        [Header("Variables")]
        public float KPH;
        public float smoothTime = 0.01f;
        public float downForceValue = 50f;
        public float gear;
        public float turnWheelRot;
        public float brakePower;
        public float boostPower;
        public AnimationCurve animationCurve;
        
        public float totalPower;
        public float wheelsRPM;
        public float engineRPM;

        [Header("DEBUG")]
        public float[] slip = new float[4];

        private void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            carManager = this.GetComponent<CarManager>();
            inputManager = this.GetComponent<InputManager>();
            rigidbody = this.GetComponent<Rigidbody>();
            wheelColliders = carManager.wheelColliders;

            if (PlayerPrefs.GetInt("Engine1") == 2)
                gear = 3f;
            if (PlayerPrefs.GetInt("Engine2") == 2)
                gear = 4f;

            carManager.rigidbody.centerOfMass = centerOfMess;

        }

        private void FixedUpdate()
        {
            WheelRotate();
            AddDownForce();
            CalculateEnginePower();
            
            GetFriction();
        }

        private void CalculateEnginePower()
        {
            WheelRPM();

            totalPower = animationCurve.Evaluate(engineRPM) * 2 * inputManager.vertical;
            float velocity = 0f;
            engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * 2), ref velocity, smoothTime);
            
            RunCar(); 
        }

        private void WheelRPM()
        {
            float sum = 0;
            int R;
            for (R = 0; R < 4; R++)
            {
                sum += wheelColliders[R].rpm;
            }

            wheelsRPM = sum / R;
        }

        private void RunCar()
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
                wheelColliders[i].motorTorque = totalPower / (ba-fr);
            }

            KPH = rigidbody.velocity.magnitude * 3.6f;

            foreach (var t in wheelColliders)
            {
                t.brakeTorque = inputManager.handbreak ? brakePower : 0;
            }
        }

        private void WheelRotate()
        {
            float hori = inputManager.horizontal;
            wheelColliders[hori < 0 ? 0 : 1].steerAngle = hori * Mathf.Rad2Deg * Mathf.Atan(2.55f / (turnWheelRot - 0.75f));
            wheelColliders[hori < 0 ? 1 : 0].steerAngle = hori * Mathf.Rad2Deg * Mathf.Atan(2.55f / (turnWheelRot + 0.75f));
        }

        private void AddDownForce()
        {
            rigidbody.AddForce(-transform.up * (downForceValue * rigidbody.velocity.magnitude));
        }

        public void SetBoost(float _boostPower)
        {
            boostPower = _boostPower;
            rigidbody.AddForce(transform.forward * boostPower, ForceMode.VelocityChange);
            CancelInvoke(nameof(StopBoost));
            Invoke(nameof(StopBoost), 2f);
        }
        private void StopBoost()
        {
            boostPower = 0;
        }

        private void GetFriction()
        {
            for (int i = 0; i < wheelColliders.Count; i++)
            {
                WheelHit wheelHit;
                wheelColliders[i].GetGroundHit(out wheelHit);

                slip[i] = wheelHit.forwardSlip;
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + centerOfMess, 0.1f);
        }
    }
}
