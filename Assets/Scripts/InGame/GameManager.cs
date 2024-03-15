using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MinGun
{
    public class GameManager : MonoBehaviour
    {
        private CarControl carControl;
        public GameObject needle;
        public Text rpmText;

        private float startPosition = 220f, endPosition = -45f;
        private float desiredPosition;

        public float vehicleSpeed;
        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
        }

        private void Update()
        {
            rpmText.text = $"{carControl.KPH:0}KPH";

            vehicleSpeed = carControl.KPH;
            UpdateNeedle();
        }

        private void UpdateNeedle()
        {
            desiredPosition = startPosition - endPosition;
            float temp = vehicleSpeed / 180;
            needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
        }
    }
}
