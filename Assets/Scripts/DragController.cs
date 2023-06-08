using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private static DragController _instance;
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    public static DragController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DragController>();
            }
            return _instance;
        }
    }

    void Update()
    {
        //check if an object was being dragged but the user just stopped dragging. If so, then drop
        if (_isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }
        // Track mouse/touch position if required
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount == 1) { 
            _screenPosition = Input.GetTouch(0).position;
        }
        // Exit if the user is not dragging
        else
        {
            return;
        }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        // Drag if _isDragActive is true
        if (_isDragActive == true)
        {
            Drag();
        }
        // Else, user might just be starting dragging, try to find Draggable object
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(_worldPosition.x, _worldPosition.y), Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    /* Dragging/Dropping methods */
    
    // Start Dragging
    private void InitDrag()
    {
        _isDragActive = true;
    }
    // Drag Object
    private void Drag()
    {
        _lastDragged.PlayerDragging(new Vector2(_worldPosition.x, _worldPosition.y));
    }
    // Start Dragging
    private void Drop()
    {
        _isDragActive = false;
    }
}


