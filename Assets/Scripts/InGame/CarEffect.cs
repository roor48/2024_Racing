using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CarEffect : MonoBehaviour
    {
        public ParticleSystem[] smoke;
        private CarControl carControl;
        private void Start()
        {
            carControl = this.GetComponent<CarControl>();
        }

        private void FixedUpdate()
        {
            if (carControl.playPauseSmoke)
                StartSmoke();
            else
                StopSmoke();
        }

        public void StartSmoke()
        {
            for (int i = 0; i < smoke.Length; i++)
                smoke[i].Play();
        }

        public void StopSmoke()
        {
            for (int i = 0; i < smoke.Length; i++)
                smoke[i].Stop();
        }
    }
}
