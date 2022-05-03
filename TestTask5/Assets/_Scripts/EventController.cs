using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    private List<Action> _events = new List<Action>();
    public void AddEvent(Action newEvent)
    {
        _events.Add(newEvent);
    }
    public void EventsInvoke()
    {
        for (int i = 0; i < _events.Count; i++)
        {
            if (_events[i] == null)
            {
                _events.RemoveAt(i);
                i--;
                continue;
            }
            _events[i]?.Invoke();
        }
    }
}
