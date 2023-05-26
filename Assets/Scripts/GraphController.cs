using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // grid
    [Range(0, 1)] [SerializeField] float padding;
    [SerializeField] int xSize;
    int ySize;
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

    private void Start()
    {
        initGrid();
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
        // y axis is 50% larger than x axis
        ySize = Mathf.FloorToInt(xSize * 1.5f);
        Camera mainCamera = Camera.main;
        // calculate available screen dimensions (remove padding)
        float screenHeightInUnits = mainCamera.orthographicSize * 2f * (1- padding);
        float screenWidthInUnits = screenHeightInUnits * mainCamera.aspect * (1 - padding);
        //calculate distance between vertices
        increment = screenWidthInUnits / xSize;

        //create grid
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newVertice = Instantiate(verticePrefab, new Vector3(x * increment, y * increment, 0f), Quaternion.identity);
                //vertices are children of the graph
                newVertice.transform.parent = gameObject.transform;
            }
        }
        //move the graph so that it centers the screen
        gameObject.transform.position = new Vector3(transform.position.x - screenWidthInUnits/2 + increment/2, transform.position.y - ((ySize - xSize) * increment), transform.position.z);
    }
}
