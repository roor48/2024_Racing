using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MinGun
{
    public class ShowNextCar : MonoBehaviour
    {
        public List<GameObject> carList;
        
        private int idx = 0;
        public int Idx
        {
            get => idx;
            set
            {
                idx += value;
                if (idx >= carList.Count)
                    idx = 0;
                else if (idx < 0)
                    idx = carList.Count - 1;
            }
        }

        public void Next(int val)
        {
            Idx += val;

            for (int i = 0; i < carList.Count; i++)
            {
                carList[i].SetActive(i==Idx);
            }

        }
    }
}
