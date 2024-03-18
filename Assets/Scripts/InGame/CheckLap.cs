using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CheckLap : MonoBehaviour
    {
        public int maxLap;

        private void OnTriggerEnter(Collider other)
        {
            CarManager carManager;
            TryGetComponent<CarManager>(out carManager);
            if (carManager == null)
                return;
            
            
        }
    }
}
