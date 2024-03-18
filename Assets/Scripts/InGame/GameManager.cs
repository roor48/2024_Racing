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

        public bool canStart;
        public Animator countDownAnim;
        private Text countDownText;
        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
            countDownText = countDownAnim.GetComponent<Text>();
            
            canStart = false;
            StartCoroutine(CountDown());
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

        private IEnumerator CountDown()
        {
            countDownText.text = "3";
            countDownAnim.SetTrigger("doCountDown");
            yield return new WaitForSeconds(1f);
            
            countDownText.text = "2";
            countDownAnim.SetTrigger("doCountDown");
            yield return new WaitForSeconds(1f);
            
            countDownText.text = "1";
            countDownAnim.SetTrigger("doCountDown");
            yield return new WaitForSeconds(1f);
            
            countDownText.text = "Go!";
            countDownAnim.SetTrigger("doCountDown");
            canStart = true;
        }
    }
}
