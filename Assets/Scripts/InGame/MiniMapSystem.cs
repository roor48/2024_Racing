using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class MiniMapSystem : MonoBehaviour
    {
        public float height = 60;
        private Transform followObj;

        private void Start()
        {
            followObj = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            Vector3 followPos = followObj.position;
            transform.position = new Vector3(followPos.x, height, followPos.z);
        }
    }
}
