using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MinGun
{
    public class Item
    {
        public enum ItemType
        {
            LowBoost,
            HighBoost
        }
        public ItemType itemType;
    }
    public class ItemManager : MonoBehaviour
    {
        private CarControl carControl;

        private void Start()
        {
            carControl = GameObject.Find("Player").GetComponent<CarControl>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                carControl.SetBoost(5f);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                switch (other.GetComponent<Item>().itemType)
                {
                    case Item.ItemType.LowBoost:
                        carControl.SetBoost(10f);
                        break;
                    case Item.ItemType.HighBoost:
                        carControl.SetBoost(5f);
                        break;
                }
            }
        }
    }
}
