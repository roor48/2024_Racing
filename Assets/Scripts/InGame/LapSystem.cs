using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MinGun
{
    public class LapSystem : MonoBehaviour
    {
        private GameFinishManager gameFinishManager;
        
        public Driver driverType;
        
        [Range(0, 10)] public int wayPointOffset;

        private int maxLap;
        public int currentLap = 0;
        public List<float> lapTimeList = new List<float>();

        public float GetFinishTime => isFinished ? lapTimeList.Sum() : Mathf.Infinity;

        public List<Transform> nodes = new List<Transform>();
        public List<Transform> visitedNodes = new List<Transform>();

        public Transform currentWaypoint;

        private float curTime = 0f;
        private Text curTimeText;
        private Text bestTimeText;

        public bool isFinished = false;

        private void Start()
        {
            gameFinishManager = GameObject.FindObjectOfType<GameFinishManager>();
            driverType = this.GetComponent<InputManager>().driverType;
            nodes = GameObject.FindObjectOfType<TrackWayPoint>().nodes;
            maxLap = GameObject.FindObjectOfType<CheckLap>().maxLap;

            curTimeText = GameObject.Find("CurTimeText").GetComponent<Text>();
            bestTimeText = GameObject.Find("BestTimeText").GetComponent<Text>();
            
            currentWaypoint = nodes[0];

            for (int i = 0; i <= wayPointOffset; i++)
                visitedNodes.Add(nodes[i]);
        }

        private void Update()
        {
            curTime += Time.deltaTime;
            
            CalculateDistanceOfWaypoints();
            UpdateTimeText();
        }

        private void CalculateDistanceOfWaypoints()
        {
            Vector3 position = transform.position;
            float distance = Mathf.Infinity;

            for (int i = 0; i < nodes.Count; i++)
            {
                // Vector3 difference = nodes[i].transform.position - position;
                // float currentDistance = difference.magnitude;

                float currentDistance = Vector3.Distance(nodes[i].position, position);
                
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

        public void FinishLap()
        {
            visitedNodes.Clear();
            currentLap++;
            
            for (int i = 0; i <= wayPointOffset; i++)
                visitedNodes.Add(nodes[i]);

            lapTimeList.Add(curTime - lapTimeList.Sum());

            if (currentLap == maxLap)
            {
                isFinished = true;
                StartCoroutine(gameFinishManager.GameFinish(this));
            }
        }

        private void UpdateTimeText()
        {
            if (driverType == Driver.Enemy)
                return;
            if (!GameManager.Instance.canStart)
                return;

            curTimeText.text = TimeSpan.FromSeconds(curTime).ToString(@"mm\:ss\.ff");

            float bestLapTime = lapTimeList.Count == 0? 0f : lapTimeList.Min();
         
            bestTimeText.text = TimeSpan.FromSeconds(bestLapTime).ToString(@"mm\:ss\.ff");
        }
        private void OnDrawGizmos()
        {
            if (currentWaypoint)
                Gizmos.DrawWireSphere(currentWaypoint.position, 3);
        }
    }
}
