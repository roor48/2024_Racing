using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class CheckLap : MonoBehaviour
    {
        [Min(1)] public int maxLap;

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out InputManager inputManager);
            if (inputManager == null)
                return;

            if (inputManager.visitedNodes.Count == inputManager.nodes.Count)
            {
                inputManager.currentLap++;
                inputManager.visitedNodes.Clear();
                
                for (int i = 0; i <= inputManager.wayPointOffset; i++)
                    inputManager.visitedNodes.Add(inputManager.nodes[i]);

            }

            if (inputManager.currentLap == maxLap)
            {
                
            }
        }
    }
}
