using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CheckLap : MonoBehaviour
    {
        [Range(1, 5)] public int maxLap;

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out LapSystem lapSystem);
            if (lapSystem == null)
                return;
            
            if (lapSystem.visitedNodes.Count < lapSystem.nodes.Count)
                return;
            
            lapSystem.FinishLap();
        }
    }
}
