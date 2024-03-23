using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MinGun
{
    public class StartManager : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetString("NextScene", "Desert");
        }

        public void OnPlayButton()
        {
            SceneManager.LoadScene("ChooseCar");
        }
    }
}
