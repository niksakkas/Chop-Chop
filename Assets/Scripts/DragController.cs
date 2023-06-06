using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool _isDragActive = false;

    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    void Awake()
    {

    }
    void Update()
    {
        if (_isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount == 1) { 
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (_isDragActive == true)
        {
            Drag();
        }
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
    private void InitDrag()
    {
        _isDragActive = true;
    }
    private void Drag()
    {
        _lastDragged.gameObject.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }
    private void Drop()
    {
        _isDragActive = false;
    }
}


