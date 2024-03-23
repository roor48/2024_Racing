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
        public Text nextMapText;
        private string nextScene;

        private void Awake()
        {
            nextScene = PlayerPrefs.GetString("NextScene");
            if (string.IsNullOrEmpty(nextScene))
            {
                PlayerPrefs.SetString("NextScene", "Desert");
                nextScene = "Desert";
            }

            Time.timeScale = 1f;
            GameManager.Instance.canStart = false;
            GameManager.Instance.isFinished = false;
            GameManager.Instance.isGameEnd = false;
            
            UpdateUIs();
        }

        public void PlayBtn()
        {
            SceneManager.LoadScene(nextScene + "Map");
        }

        private void UpdateUIs()
        {
            nextMapText.text = $"NextMap: {nextScene}";
        }
    }
}
