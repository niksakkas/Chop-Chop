using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    /* Move Subscription Handling */

    // Action delegate for the event
    private Action<Vector2> notifyEvent;
    // Subscribe a listener to the event
    public void Subscribe(Action<Vector2> listener)
    {
        notifyEvent += listener;
    }
    // Unsubscribe a listener from the event
    public void Unsubscribe(Action<Vector2> listener)
    {
        notifyEvent -= listener;
    }
    // Trigger the event and notify all subscribers
    public void PlayerDragging(Vector2 newPos)
    {
        notifyEvent?.Invoke(newPos);
    }
}
