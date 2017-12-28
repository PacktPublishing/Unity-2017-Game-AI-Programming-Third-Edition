using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour 
{
    private Transform startPosition;
    private Transform endPosition;

    public Node startNode { get; set; }
    public Node goalNode { get; set; }

    private ArrayList pathArray;

    private GameObject startCube;
    private GameObject endCube;
	
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; 
    private GridManager gridManager;
    
	private void Start () 
    {
        gridManager = FindObjectOfType<GridManager>();
        startCube = GameObject.FindGameObjectWithTag("Start");
        endCube = GameObject.FindGameObjectWithTag("End");

        //Calculate the path using our AStart code.
        pathArray = new ArrayList();
        FindPath();
	}
	
	private void Update () 
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }
	}

    private void FindPath()
    {
        startPosition = startCube.transform;
        endPosition = endCube.transform;
        
        startNode = new Node(gridManager.GetGridCellCenter(gridManager.GetGridIndex(startPosition.position)));
        goalNode = new Node(gridManager.GetGridCellCenter(gridManager.GetGridIndex(endPosition.position)));

        pathArray = AStar.FindPath(startNode, goalNode);
    }

    private void OnDrawGizmos()
    {
        if (pathArray == null) 
        {
            return;
        }

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            };
        }
    }
}