using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private List<GameObject> value;
        private EventController _eventController = new EventController();
        public void AddEvent(Action blockEvent)
        {
            _eventController.AddEvent(blockEvent);
        }
        private void InvokeEvents()
        {
            _eventController.EventsInvoke();
        }
        public void OnClick()
        {
            InvokeEvents();
        }
        public bool SetBlock(int valueId)
        {
            for(int i = 0; i < value.Count; i++)
            {
                if(value[i].activeSelf) return false;
            }
            if(valueId > value.Count || valueId < 0) 
            {
                Debug.LogError("Non correct id");
                return false;
            }
            value[valueId].SetActive(true);
            return true;
        }
    }
}