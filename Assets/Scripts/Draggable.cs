using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public void Move(Vector2 newPos)
    {
        gameObject.transform.position = newPos;
    }
}
