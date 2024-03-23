using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class TrackWayPoint : MonoBehaviour
    {
        public Color lineColor;
        [Range(0, 1)] public float sphereRadius;
        public List<Transform> nodes = new List<Transform>();

        private void OnDrawGizmos()
        {
            Gizmos.color = lineColor;
            Transform[] path = GetComponentsInChildren<Transform>();

            nodes = new List<Transform>();
            for (int i = 1; i < path.Length; i++)
            {
                path[i].name = $"{i}";
                nodes.Add(path[i]);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 currentWaypoint = nodes[i].position;
                Vector3 previousWaypoint = nodes[^1].position;

                if (i > 0)
                    previousWaypoint = nodes[i - 1].position;
                
                Gizmos.DrawLine(previousWaypoint, currentWaypoint);
                Gizmos.DrawSphere(currentWaypoint, sphereRadius);
            }
        }
    }
}
