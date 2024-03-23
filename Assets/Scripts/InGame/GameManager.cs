using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MinGun
{
    public class GameManager : Singleton<GameManager>
    {
        public bool canStart = false;

        public bool isFinished = false;
        public bool isGameEnd = false;

        public void SetTimeScale(float val)
        {
            if (isGameEnd)
                val = 0f;
            Time.timeScale = val;
        }
        public void GetMoney(int _money)
        {
            Debug.Log($"GetMoney: {_money}");
            PlayerPrefs.SetInt("Currency", _money + PlayerPrefs.GetInt("Currency"));
        }
        
        public void GoNextScene()
        {
            string curScene = PlayerPrefs.GetString("NextScene");
            string nextScene = "";
            
            switch (curScene)
            {
                case "Desert":
                    nextScene = "Forest";
                    break;
                
                case "Forest":
                    nextScene = "City";
                    break;
                
                case "City":
                    nextScene = "End";
                    break;
                
                default:
                    nextScene = "Desert";
                    break;
            }

            PlayerPrefs.SetString("NextScene", nextScene);

            canStart = false;
            isFinished = false;
            isGameEnd = false;
            SetTimeScale(1f);

            if (nextScene == "End")
            {
                SceneManager.LoadScene("EndMap");
            }
            else
            {
                SceneManager.LoadScene("ChooseCar");
            }
        }
    }
}
