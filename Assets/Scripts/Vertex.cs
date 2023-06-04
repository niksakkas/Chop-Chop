using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public int id { get; set; }
    public GameObject gameobject { get; set; }
    public HashSet<Vertex> neighbours;


    public Vertex(int _id, GameObject _gameobject)
    {
        id = _id;
        gameobject = _gameobject;
        neighbours = new HashSet<Vertex>();
    }
}