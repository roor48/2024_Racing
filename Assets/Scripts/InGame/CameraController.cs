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

        public float defaltFOV = 0, desiredFOV = 0;
        [Range(0, 5)]public float smoothTime = 0;
        private void Awake()
        {
            mainCam = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            carControl = player.GetComponent<CarControl>();
        }

        private void FixedUpdate()
        {
            Follow();
            BoostFOV();
        }

        private void Follow()
        {
            if (speed <= 23)
                speed = Mathf.Lerp(speed, carControl.KPH / 4f, Time.fixedDeltaTime);
            else
                speed = 23;
            transform.position = Vector3.Lerp(transform.position, cameraPos.position, Time.fixedDeltaTime * speed);
            transform.LookAt(lookPos.position);
        }

        private void BoostFOV()
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, carControl.boostPower == 0 ? defaltFOV: desiredFOV, Time.fixedDeltaTime * smoothTime);
        }
    }
}
