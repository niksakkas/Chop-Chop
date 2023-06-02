using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public int id { get; set; }
    GameObject vertex { get; set; }
    public HashSet<Vertex> neighbours;


    public Vertex(int _id, GameObject _vertex)
    {
        id = _id;
        vertex = _vertex;
    }

}

