using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    // grid
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float increment;
    [SerializeField] GameObject verticePrefab;
    // graph
    LinkedList<Edge>[] adjLists;
   
    void Start()
    {

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
