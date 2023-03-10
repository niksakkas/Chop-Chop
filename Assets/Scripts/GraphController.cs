using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // grid
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float increment;
    [SerializeField] GameObject verticePrefab;
    // graph
    int vertices;
    LinkedList<int>[] adjList;
   
    Graph() {
        adjList = new LinkedList<int>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            adjList[i] = new LinkedList<int>();
        }
    }

    public void addEdge(int source, int destination)
    {
        //forward edge
        adjList[source].AddFirst(destination);
        //backward edge in undirected graph
        adjList[destination].AddFirst(source);
    }
    public void removeEdge(int source, int destination)
    {
        //remove forward edge
        for (int i = 0; i < adjList[source].Count; i++)
        {
                adjList[source].Remove(destination);
        }
        //remove backward edge in undirected graph
        for (int i = 0; i < adjList[destination].Count; i++)
        {
            adjList[destination].Remove(source);
        }
    }

    void initGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newEdge = Instantiate(verticePrefab, new Vector3(x / increment, y / increment, 0f), Quaternion.identity);
                newEdge.transform.parent = gameObject.transform;
            }
        }
        gameObject.transform.position = new Vector3((transform.position.x - xSize / (2 * increment)) + 0.5f * (1 / increment), transform.position.y, transform.position.z);

    }
}
