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

    public enum WheelType
    {
        None,
        Desert,
        Forest,
        City
    }
    [RequireComponent(typeof(CarManager)), RequireComponent(typeof(WheelAnim)), RequireComponent(typeof(InputManager))]
    public class CarControl : MonoBehaviour
    {
        private CarManager carManager;
        private GameManager gameManager;
        private Rigidbody rigidbody;
        [HideInInspector] public InputManager inputManager;
        
        private List<WheelCollider> wheelColliders;
        
        public CarType carType;
        public Vector3 centerOfMess;

        [Header("Variables")]
        public float handBrakeFrictionMultiplier = 2f;
        public float KPH;
        public float gear;
        public float turnWheelRot;
        public float boostPower;
        public AnimationCurve animationCurve;
        
        public float totalPower;
        public float wheelsRPM;
        public float engineRPM;

        public WheelType wheelType;

        private WheelFrictionCurve forwardFriction, sidewaysFriction;
        private float thrust = -2000, radius = 6, brakePower = 50000, downForceValue = 100f, smoothTime = 0.09f, throttle;
        
        [Header("DEBUG")]
        public float[] slip = new float[4];

        private void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            carManager = this.GetComponent<CarManager>();
            inputManager = this.GetComponent<InputManager>();
            rigidbody = this.GetComponent<Rigidbody>();
            wheelColliders = carManager.wheelColliders;

            if (inputManager.driverType != InputManager.Driver.AI)
            {
                if (PlayerPrefs.GetInt("6Engine") == 2)
                    gear = 3f;
                else if (PlayerPrefs.GetInt("8Engine") == 2)
                    gear = 4f;
                else
                    gear = 2f;

                if (PlayerPrefs.GetInt("DesertWheel") == 2)
                    wheelType = WheelType.Desert;
                else if (PlayerPrefs.GetInt("ForestWheel") == 2)
                    wheelType = WheelType.Forest;
                else if (PlayerPrefs.GetInt("CityWheel") == 2)
                    wheelType = WheelType.City;
                else
                    wheelType = WheelType.None;
            }

            carManager.rigidbody.centerOfMass = centerOfMess;
        }

        private void FixedUpdate()
        {
            WheelRotate();
            AddDownForce();
            CalculateEnginePower();
            AdjustTraction();
            
            GetFriction();
        }

        private void CalculateEnginePower()
        {
            WheelRPM();

            totalPower = animationCurve.Evaluate(engineRPM) * gear * inputManager.vertical;
            float velocity = 0f;
            engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * gear), ref velocity, smoothTime);
            
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

            if(carType == CarType.all)
            {
                foreach (var t in wheelColliders)
                {
                    t.motorTorque = totalPower / 4;
                }
            }
            else if(carType == CarType.rear)
            {
                for (int i = 2; i < wheelColliders.Count; i++)
                {
                    wheelColliders[i].motorTorque = totalPower / 2;
                }
            }
            else if (carType == CarType.front)
            {
                for (int i = 0 ; i < wheelColliders.Count - 2; i++)
                {
                    wheelColliders[i].motorTorque =  totalPower / 2;
                }  
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
            rigidbody.AddForce(-transform.up * (downForceValue * rigidbody.velocity.magnitude));
        }

        private float driftFactor;
        private void AdjustTraction()
        {
            float driftSmoothFactor = .7f * Time.deltaTime;
            if (inputManager.handbreak)
            {
                sidewaysFriction = wheelColliders[0].sidewaysFriction;
                forwardFriction = wheelColliders[0].forwardFriction;

                float velocity = 0f;
                sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue
                        = Mathf.SmoothDamp(forwardFriction.asymptoteValue, driftFactor * handBrakeFrictionMultiplier,
                                            ref velocity, 0.7f * driftSmoothFactor);

                for (int i = 2; i < 4; i++)
                {
                    wheelColliders[i].sidewaysFriction = sidewaysFriction;
                    wheelColliders[i].forwardFriction = forwardFriction;
                }

                forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue
                    = 1.1f;

                for (int i = 0; i < 2; i++)
                {
                    wheelColliders[i].sidewaysFriction = sidewaysFriction;
                    wheelColliders[i].forwardFriction = forwardFriction;
                }
                rigidbody.AddForce(transform.forward * (KPH * 25));
            }
            else
            {
                forwardFriction = wheelColliders[0].forwardFriction;
                sidewaysFriction = wheelColliders[0].sidewaysFriction;

                sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =
                    KPH * handBrakeFrictionMultiplier / 300f + 1;

                for (int i = 0; i < 4; i++)
                {
                    wheelColliders[i].forwardFriction = forwardFriction;
                    wheelColliders[i].sidewaysFriction = sidewaysFriction;
                }
            }

            for (int i = 2; i < 4; i++)
            {
                WheelHit wheelHit;
                wheelColliders[i].GetGroundHit(out wheelHit);

                if (wheelHit.sidewaysSlip >= 0.3f || wheelHit.sidewaysSlip <= -0.3f ||
                    wheelHit.forwardSlip >= 0.3f || wheelHit.forwardSlip <= -0.3f)
                {
                    playPauseSmoke = true;
                }
                else
                {
                    playPauseSmoke = false;
                }

                if (wheelHit.sidewaysSlip < 0)
                    driftFactor = (1 + -inputManager.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
                else if (wheelHit.sidewaysSlip > 0)
                    driftFactor = (1 + inputManager.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);

            }
        }

        public bool playPauseSmoke = false;

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
