using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CameraController : MonoBehaviour
    {
        private Camera mainCam;
        private Transform player;
        private CarControl carControl;
        
        public float speed;
        public Transform cameraPos;
        public Transform lookPos;

        public float defaultFOV = 0, desiredFOV = 0;
        [Range(0, 5)]public float smoothTime = 0;
        private void Awake()
        {
            mainCam = this.GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            cameraPos = player.transform.Find("CameraPos");
            lookPos = player.transform.Find("LookPos");
            carControl = player.GetComponent<CarControl>();

            desiredFOV = (120f - defaultFOV) / 5f;
        }

        private void FixedUpdate()
        {
            Follow();
            BoostFOV();
        }

        private void Follow()
        {
            if (speed < 23)
                speed = Mathf.Lerp(speed, carControl.KPH / 4f, Time.fixedDeltaTime);
            else
                speed = 23;
            transform.position = Vector3.Lerp(transform.position, cameraPos.position, Time.fixedDeltaTime * speed);

            transform.LookAt(lookPos.position);
        }

        private void BoostFOV()
        {
            if (carControl.inputManager.handbreak)
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, defaultFOV, Time.fixedDeltaTime * smoothTime);
            else if (carControl.boostPower == 0)
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, defaultFOV + speed * 2, Time.fixedDeltaTime * smoothTime);
            else
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, defaultFOV + desiredFOV * carControl.boostPower, Time.fixedDeltaTime * smoothTime);
        }
    }
}
