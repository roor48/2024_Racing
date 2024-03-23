using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MinGun
{
    public class CheatManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                // 아이템 나열 후 선택
                try
                {
                    GameObject.Find("Canvas").transform.GetChild(1).Find("ItemBG").gameObject.SetActive(true);
                }
                catch
                {
                    // ignored
                }
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                // 상점 무료
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                // 재시작
                Scene curScene = SceneManager.GetActiveScene();
                if (curScene == SceneManager.GetSceneByName("DesertMap") ||
                    curScene == SceneManager.GetSceneByName("ForestMap") ||
                    curScene == SceneManager.GetSceneByName("CityMap"))
                {
                    SceneManager.LoadScene(curScene.name);
                }
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StartGame") ||
                    SceneManager.GetActiveScene() == SceneManager.GetSceneByName("EndMap"))
                    return;
                // 스테이지 이동
                string curScene = PlayerPrefs.GetString("NextScene");
                string nextScene = "";
                
                Debug.Log(curScene);
            
                switch (curScene)
                {
                    case "Desert":
                        nextScene = "Forest";
                        break;
                
                    case "Forest":
                        nextScene = "City";
                        break;
                
                    case "City":
                        nextScene = "Desert";
                        break;
                
                    default:
                        nextScene = "Desert";
                        break;
                }

                PlayerPrefs.SetString("NextScene", nextScene);
            
                Time.timeScale = 1f;
                
                SceneManager.LoadScene("ChooseCar");
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                // 일시 정지
                Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
            }
        }
    }
}
