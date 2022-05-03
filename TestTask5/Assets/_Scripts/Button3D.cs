using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button3D : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick; 
   
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        onClick.Invoke();
    }
}