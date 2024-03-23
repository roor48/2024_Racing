using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinGun
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<T>();
                if (Instance == null)
                {
                    GameObject temp = new GameObject();
                    Instance = temp.AddComponent<T>();
                }
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
