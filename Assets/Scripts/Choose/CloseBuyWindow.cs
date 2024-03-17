using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MinGun
{
    public class CloseBuyWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject buyWindow;
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            buyWindow.SetActive(false);
        }
    }
}
