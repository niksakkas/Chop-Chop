using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private HashSet<GameObject> edges;

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
}