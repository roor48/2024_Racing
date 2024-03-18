using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace MinGun
{
    public class InputManager : MonoBehaviour
    {
        public enum Driver
        {
            AI,
            KeyBoard,
        }
        public Driver driverType;
        public float horizontal;
        public float vertical;
        public bool handbreak;

        public Transform currentWaypoint;
        public int currentLap = 0;
        public List<Transform> nodes;
        public List<Transform> visitedNodes = new List<Transform>();
        
        [Range(0, 10)] public int wayPointOffset;
        [Range(0, 5)] public float steerForce;
        private void Awake()
        {
            nodes = GameObject.FindWithTag("Path").GetComponent<TrackWayPoint>().nodes;
            currentWaypoint = nodes[0];
            
            for (int i = 0; i <= wayPointOffset; i++)
                visitedNodes.Add(nodes[i]);
        }

        private void Update()
        {
            switch (driverType)
            {
                case Driver.AI:
                    OnAI();
                    break;
                
                case Driver.KeyBoard:
                    OnKeyBoard();
                    break;
            }
            
            CalculateDistanceOfWaypoints();
        }
        private void OnKeyBoard()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            handbreak = Input.GetAxis("Jump") != 0;
        }

        private void OnAI()
        {
            Vector3 relative = transform.InverseTransformPoint(currentWaypoint.transform.position);
            relative /= relative.magnitude;

            horizontal = relative.x / relative.magnitude * steerForce;
        }

        private void CalculateDistanceOfWaypoints()
        {
            Vector3 position = transform.position;
            float distance = Mathf.Infinity;

            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 difference = nodes[i].transform.position - position;
                float currentDistance = difference.magnitude;

                if (currentDistance < distance)
                {
                    int idx = i + wayPointOffset;
                    if (idx >= nodes.Count)
                        idx -= nodes.Count;
                    currentWaypoint = nodes[idx];
                    distance = currentDistance;
                }
            }
            if ((int.Parse(visitedNodes[^1].name) + 1).ToString() == currentWaypoint.name)
            {
                visitedNodes.Add(currentWaypoint);
            }
        }

        private void OnDrawGizmos()
        {
            if (currentWaypoint)
                Gizmos.DrawWireSphere(currentWaypoint.position, 3);
        }
    }
}
