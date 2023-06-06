using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField]
    private GameObject startVertex;

    [SerializeField]
    private GameObject endVertex;

    public GameObject StartVertex
    {
        get { return startVertex; }
        set { startVertex = value; }
    }

    public GameObject EndVertex
    {
        get { return endVertex; }
        set { endVertex = value; }
    }
}
