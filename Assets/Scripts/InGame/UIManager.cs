using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MinGun
{
    public class UIManager : MonoBehaviour
    {
        public CarControl carControl;
        
        public Text countDownText;
        public Animator countDownAnim;
        
        public GameObject needle;
        public Text rpmText;
        
        public float vehicleSpeed;
        private float startPosition = 220f, endPosition = -45f;
        private float desiredPosition;

        private Transform canvas;
        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
            canvas = GameObject.Find("Canvas").transform;
            
            countDownAnim = GameObject.Find("CountDown").GetComponent<Animator>();
            countDownText = countDownAnim.GetComponent<Text>();
            
            StartCoroutine(CountDown());
        }

        private void Update()
        {
            UpdateNeedle();
            SetTimeScale();
        }
        
        private void UpdateNeedle()
        {
            rpmText.text = $"{carControl.KPH:0}KPH";

            vehicleSpeed = carControl.KPH;

            desiredPosition = startPosition - endPosition;
            float temp = vehicleSpeed / 180;
            needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
        }

        private void SetTimeScale()
        {
            for (int i = 0; i < canvas.GetChild(1).childCount; i++)
                if (canvas.GetChild(1).GetChild(i).gameObject.activeSelf)
                {
                    GameManager.Instance.SetTimeScale(0f);
                    return;
                }
            GameManager.Instance.SetTimeScale(1f);
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

            GameManager.Instance.canStart = true;
        }
    }
}
