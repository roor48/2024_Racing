using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MinGun
{
    public class ItemManager : MonoBehaviour
    {
        private CarControl carControl;
        private GameManager gameManager;

        public GameObject upgradeWindow;
        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                GetBoost(8f);
            
            if (Input.GetKeyDown(KeyCode.E))
                GetBoost(4f);
        }

        public void GetMoney(int _money)
        {
            gameManager.GetMoney(_money);
        }

        public void GetBoost(float _speed)
        {
            carControl.SetBoost(_speed);
        }

        public void ShowUpgradeWindow()
        {
            upgradeWindow.SetActive(true);
        }
    }
}
