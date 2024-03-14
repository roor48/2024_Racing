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
        private Rigidbody playerRigid;
        
        public float speed;
        public Transform cameraPos;
        public Transform lookPos;
        
        private void Awake()
        {
            mainCam = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerRigid = player.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Follow();
            SetFOV();
        }

        private void Follow()
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos.position, Time.fixedDeltaTime * speed);
            transform.LookAt(lookPos.position);
        }

        private void SetFOV()
        {
            mainCam.fieldOfView = 50 + Mathf.Clamp(playerRigid.velocity.magnitude * 3f, 0f, 50f);
        }
    }
}
