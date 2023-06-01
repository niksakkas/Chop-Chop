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
    [SerializeField] GameObject verticePrefab;
    // graph
    LinkedList<Vertice>[] graph;


    private void Start()
    {
        initGraph();
    }

    void initGraph()
    {
        // y axis is 50% larger than x axis
        ySize = Mathf.FloorToInt(xSize * 1.5f);
        Camera mainCamera = Camera.main;
        // calculate available screen dimensions (remove padding)
        float screenHeightInUnits = mainCamera.orthographicSize * 2f * (1- padding);
        float screenWidthInUnits = screenHeightInUnits * mainCamera.aspect * (1 - padding);
        //calculate distance between vertices
        increment = screenWidthInUnits / xSize;
        //get the positions that the vertices will be created at
        int[] verticePositions = getVerticePositions();
        //create the vertices
        createVertices(verticePositions);
        //move the graph to the correct position
        gameObject.transform.position = new Vector3(transform.position.x - screenWidthInUnits/2 + increment/2, transform.position.y - ((ySize - xSize) * increment), transform.position.z);
    }

    int[] getVerticePositions()
    {
        if(xSize * ySize < numOfVertices)
        {
            Debug.LogError("Cant add more vertices than graph size");
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
                randomNumber = Random.Range(0, xSize*ySize);
            } while (randomNumbers.Contains(randomNumber));
            // Add the unique random number to the list
            randomNumbers.Add(randomNumber);
            // Display the generated number
                 }
        return randomNumbers.ToArray();

    }
    
    void createVertices(int[] verticePositions)
    {
        for (int i = 0; i < verticePositions.Length; i++)
        {
            int x = verticePositions[i] % xSize;
            int y = verticePositions[i] / ySize;
            GameObject newVertice = Instantiate(verticePrefab, new Vector3(x * increment, y * increment, 0f), Quaternion.identity);
            //vertices are children of the graph
            newVertice.transform.parent = gameObject.transform;
        }
    }

}
