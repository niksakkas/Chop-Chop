using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphController : MonoBehaviour
{


    // grid
    [Range(0, 1)] [SerializeField] float padding;
    [SerializeField] int xSize;
    int ySize;
    [SerializeField] int numOfVertices;
    float increment;
    // prefabs
    [SerializeField] GameObject vertexPrefab;
    [SerializeField] GameObject edgePrefab;
    [SerializeField] GameObject verticesParent;
    [SerializeField] GameObject edgesParent;

    // graph
    List<GameObject> vertices = new List<GameObject>();

    // Singleton instance
    private static GraphController _instance;

    // Singleton accessor
    public static GraphController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GraphController>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GraphController>();
                    singletonObject.name = typeof(GraphController).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private void Start()
    {
        initGraph();
    }

    void initGraph()
    {
        // y axis is 50% larger than x axis
        ySize = Mathf.FloorToInt(xSize * 1.5f);
        Camera mainCamera = Camera.main;
        // Calculate available screen dimensions (remove padding)
        float screenHeightInUnits = mainCamera.orthographicSize * 2f * (1- padding);
        float screenWidthInUnits = screenHeightInUnits * mainCamera.aspect * (1 - padding);
        // Calculate distance between vertices
        increment = screenWidthInUnits / xSize;
        // Get the positions that the vertices will be created at, then create the vertices
        int[] verticePositions = getVerticePositions();
        createVertices(verticePositions);
        // Move the graph to the correct position
        gameObject.transform.position = new Vector3(transform.position.x - screenWidthInUnits/2 + increment/2, transform.position.y - ((ySize - xSize) * increment), transform.position.z);
        // Create the edges
        createEdges();
        //logGraph();
    }

    int[] getVerticePositions()
    {
        if(xSize * ySize < numOfVertices)
        {
            Debug.LogError("Cant add more vertices than the graph size");
            return null;
        }
        // Create a list to store the generated numbers
        var randomNumbers = new System.Collections.Generic.List<int>();
        // Generate five different random numbers
        for (int i = 0; i < numOfVertices; i++)
        {
            int randomNumber;
            // Generate a new random number and check if it already exists in the list
            do
            {
                randomNumber = UnityEngine.Random.Range(0, xSize*ySize);
            } 
            while (randomNumbers.Contains(randomNumber));
            // Add the unique random number to the list
            randomNumbers.Add(randomNumber);
            // Display the generated number
                 }
        int[] positions = randomNumbers.ToArray();
        Array.Sort(positions);
        return positions;
    }
    void createVertices(int[] verticePositions)
    {
        for (int i = 0; i < verticePositions.Length; i++)
        {
            createVertex(verticePositions[i]);
        }
    }
    void createVertex(int verticeID)
    {   
        // Calculate new vertex position
        int x = verticeID % xSize;
        int y = verticeID / xSize;
        GameObject newVerticeGameObject = Instantiate(vertexPrefab, new Vector3(x * increment, y * increment, 0), Quaternion.identity);
        newVerticeGameObject.GetComponent<Vertex>().Id = verticeID;
        newVerticeGameObject.GetComponent<Vertex>().Edges = new HashSet<GameObject>();
        // Vertices are children of the graph
        newVerticeGameObject.transform.parent = verticesParent.transform;
        vertices.Add(newVerticeGameObject);
    }
    void createEdges()
    {
        // Iterating through the graph using foreach loop
        foreach (GameObject vertex in vertices)
        {
            int randomIndex = UnityEngine.Random.Range(0, vertices.Count);
            while(vertices[randomIndex] == vertex || vertex.GetComponent<Vertex>().Edges.Any(edge => edge.GetComponent<Edge>().StartVertex == vertices[randomIndex] || edge.GetComponent<Edge>().EndVertex == vertices[randomIndex]))
            {
                randomIndex = UnityEngine.Random.Range(0, vertices.Count);
            }
            GameObject randomVertex = vertices[randomIndex];
            createEdge(vertex, randomVertex);
        }
    }
    void createEdge(GameObject vertexA, GameObject vertexB)
    {
        //Create edge
        GameObject newEdgeGameObject = Instantiate(edgePrefab);
        Edge newEdge = newEdgeGameObject.GetComponent<Edge>();
        newEdge.transform.parent = edgesParent.transform;
        newEdge.StartVertex = vertexA;
        newEdge.EndVertex = vertexB;
        // Create edge's line
        LineRenderer lineRenderer = newEdgeGameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, vertexA.transform.position);
        lineRenderer.SetPosition(1, vertexB.transform.position);
        // Create edge's collider
        EdgeCollider2D edgeCollider = newEdge.GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[]
        {
            vertexA.transform.position, 
            vertexB.transform.position,
        };
        edgeCollider.points = points;
        // Add edge to vertices hashSets
        vertexA.GetComponent<Vertex>().addEdge(newEdgeGameObject);
        vertexB.GetComponent<Vertex>().addEdge(newEdgeGameObject);
    }

    public void removeEdge(GameObject edge)
    {
        edge.GetComponent<Edge>().StartVertex.GetComponent<Vertex>().Edges.Remove(edge);
        edge.GetComponent<Edge>().EndVertex.GetComponent<Vertex>().Edges.Remove(edge);
        Destroy(edge);
    }
    void logGraph()
    {
        // For every vertice
        vertices.ForEach(vertex =>
        {
            Vertex vertexScript = vertex.GetComponent<Vertex>();
            // Get vertice id
            int vertexId = vertexScript.Id;
            // Get all pairs of the edges of that vertice
            string neighbours = "";
            foreach (GameObject edge in vertexScript.Edges)
            {
                Edge edgeScript = edge.GetComponent<Edge>();
                neighbours += "(" + edgeScript.StartVertex.GetComponent<Vertex>().Id.ToString() + ", " + edgeScript.EndVertex.GetComponent<Vertex>().Id.ToString() + ") ";
            }
            // Print them vertice by vertice
            Debug.Log(vertexId +": "+ neighbours);
        });
    }
}
