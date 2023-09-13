using System;
using UnityEngine;

public class ChopController : MonoBehaviour
{
    [SerializeField] GraphController graphController;
    public void OnChop()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Collider2D[] surroundingEdges = new Collider2D[10];
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero);

        if (hit)
        {
            if (hit.collider.CompareTag("Edge"))
            {   
                graphController.playerChop(hit.collider.gameObject);
            }
        }
    }
    public void OnSelect()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Collider2D[] surroundingEdges = new Collider2D[10];
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero);

        if (hit)
        {
            if (hit.collider.CompareTag("Edge"))
            {
                graphController.playerSelect(hit.collider.gameObject);
            }
        }
    }
}
