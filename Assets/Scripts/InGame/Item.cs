using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class Item : MonoBehaviour
    {
        public ItemManager itemManager;
        public enum ItemType
        {
            Give1Money,
            Give5Money,
            Give10Money,
            LowBoost,
            HighBoost,
            CanBuyItem,
        }
        public ItemType itemType;

        private void Start()
        {
            itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            switch (itemType)
            {
                case ItemType.Give1Money:
                    itemManager.GetMoney(10000);
                    break;
                case ItemType.Give5Money:
                    itemManager.GetMoney(50000);
                    break;
                case ItemType.Give10Money:
                    itemManager.GetMoney(100000);
                    break;
                case ItemType.LowBoost:
                    itemManager.GetBoost(4f);
                    break;
                case ItemType.HighBoost:
                    itemManager.GetBoost(8f);
                    break;
                case ItemType.CanBuyItem:
                    itemManager.ShowUpgradeWindow();
                    break;
            }
            
            gameObject.SetActive(false);
        }
    }
    
}
