using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Edge : MonoBehaviour
{
    public Vertex start { get; set; }
    public Vertex end { get; set; }
    public GameObject lineObject { get; set; }

    public Edge(Vertex _start, Vertex _end, GameObject _lineObject)
    {
        start = _start;
        end = _end;
        lineObject = _lineObject;
    }
}