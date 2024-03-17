using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MinGun
{
    public class ChooseManager : MonoBehaviour
    {
        public int currency;

        public List<Transform> buyButtons;

        public Text currencyText;
        public ShopInfo curShopInfo;
        public GameObject buyWindow;

        private void Awake()
        {
            PlayerPrefs.DeleteAll();
            currency = PlayerPrefs.GetInt("Currency");
            currency = 123456789;
            
            UpdateUIs();
        }

        public void PlayBtn()
        {
            string nextScene = PlayerPrefs.GetString("NextScene");

            if (string.IsNullOrEmpty(nextScene))
                SceneManager.LoadScene("DesertMap");
            else
                SceneManager.LoadScene(nextScene);
        }

        public void CloseUpgradeWindow(GameObject upgradeWindow)
        {
            buyWindow.SetActive(false);
            upgradeWindow.SetActive(false);
        }
        
        public void BuyWindowBtn(ShopInfo shopInfo)
        {
            curShopInfo = shopInfo;
            buyWindow.transform.GetChild(0).GetComponent<Text>().text = $"{curShopInfo.Price:#,##0}$";
            buyWindow.transform.GetChild(1).GetComponent<Text>().text = curShopInfo.Info;
            buyWindow.SetActive(true);
        }

        public void EquipBtn(GameObject name)
        {
            if (name.name.Contains("Engine"))
            {
                PlayerPrefs.SetInt("6Engine", PlayerPrefs.GetInt("6Engine") == 0 ? 0 : 1);
                PlayerPrefs.SetInt("8Engine", PlayerPrefs.GetInt("8Engine") == 0 ? 0 : 1);
            }
            else if (name.name.Contains("Wheel"))
            {
                PlayerPrefs.SetInt("DesertWheel", PlayerPrefs.GetInt("DesertWheel") == 0 ? 0 : 1);
                PlayerPrefs.SetInt("ForestWheel", PlayerPrefs.GetInt("ForestWheel") == 0 ? 0 : 1);
                PlayerPrefs.SetInt("CityWheel", PlayerPrefs.GetInt("CityWheel") == 0 ? 0 : 1);
            }

            PlayerPrefs.SetInt(name.name, 2);
            UpdateUIs();
        }

        public void UnEquipBtn(GameObject name)
        {
            PlayerPrefs.SetInt(name.name, 1);
            
            UpdateUIs();
        }

        public void BuyBtn()
        {
            if (currency < curShopInfo.Price)
            {
                Debug.Log("구매실패");
                return;
            }
            
            currency -= curShopInfo.Price;
            PlayerPrefs.SetInt(curShopInfo.Name, 1);
            
            buyWindow.SetActive(false);
            
            UpdateUIs();
            Debug.Log("구매성공");
        }

        private void UpdateUIs()
        {
            currencyText.text = $"{currency:#,##0}$";

            foreach (Transform buyButton in buyButtons)
            {
                int val = PlayerPrefs.GetInt(buyButton.name);
                // 0: 구매안함      1: 장착안함     2: 장착함
                for (int i = 0; i < 3; i++)
                {
                    buyButton.GetChild(i).gameObject.SetActive(i==val);
                }
            }
        }
    }
}
