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
        public Text rpmText;

        private float startPosition = 130f, endPosition = 230f;
        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
        }

        private void Update()
        {
            rpmText.text = $"{carControl.KPH:0}KPH";
        }
    }
}
