using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Vertex : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private HashSet<GameObject> edges;
    private Draggable draggable;

    private void Start()
    {
        SubscribeToDraggable();
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public HashSet<GameObject> Edges
    {
        get { return edges; }
        set { edges = value; }
    }

    public void addEdge(GameObject newEdge)
    {
        edges.Add(newEdge);
    }
    public void SubscribeToDraggable()
    {
        draggable = GetComponent<Draggable>();
        draggable.Subscribe(MoveVertex) ;
    }
    public void MoveVertex(Vector2 draggingPosition)
    {
        gameObject.transform.position = draggingPosition;
        foreach (GameObject edge in edges)
        {
            MoveEdge(edge, draggingPosition);
        }
    }
    public void MoveEdge(GameObject edgeGameObject, Vector2 draggingPosition)
    {
       LineRenderer lineRenderer = edgeGameObject.GetComponent<LineRenderer>();
       Edge edge = edgeGameObject.GetComponent<Edge>();
       if(edge.StartVertex == gameObject)
        {
            lineRenderer.SetPosition(0, draggingPosition);
        }
        else
        {
            lineRenderer.SetPosition(1, draggingPosition);
        }
    }
}