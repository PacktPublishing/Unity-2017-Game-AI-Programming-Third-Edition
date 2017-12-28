using UnityEngine;
using System.Collections;

public class AStar
{
    public static PriorityQueue closedList;
    public static PriorityQueue openList;
    
    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }

    
    /// Calculate the estimated Heuristic cost to the goal    
    private static float EstimateHeuristicCost(Node currentNode, Node goalNode)
    {
        Vector3 cost = currentNode.position - goalNode.position;
        return cost.magnitude;
    }
    
    // Find the path between start node and goal node using A* Algorithm
    public static ArrayList FindPath(Node start, Node goal)
    {
        openList = new PriorityQueue();
        openList.Push(start);
        start.gCost = 0.0f;
        start.hCost = EstimateHeuristicCost(start, goal);

        closedList = new PriorityQueue();
        Node node = null;

        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
        if(gridManager == null) {
            return null;
        }

        while (openList.Length != 0)
        {
            node = openList.GetFirstNode();

            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }
			
            ArrayList neighbors = new ArrayList();
            gridManager.GetNeighbors(node, neighbors);
            
            //Update the costs of each neighbor node.
            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighborNode = (Node)neighbors[i];

                if (!closedList.Contains(neighborNode))
                {					
					//Cost from current node to this neighbor node
	                float cost = EstimateHeuristicCost(node, neighborNode);	
	                
					//Total Cost So Far from start to this neighbor node
	                float totalCost = node.gCost + cost;
					
					//Estimated cost for neighbor node to the goal
	                float neighborNodeEstCost = EstimateHeuristicCost(neighborNode, goal);					
					
					//Assign neighbor node properties
	                neighborNode.gCost = totalCost;
	                neighborNode.parent = node;
	                neighborNode.hCost = totalCost + neighborNodeEstCost;
	
	                //Add the neighbor node to the open list if we haven't already done so.
	                if (!openList.Contains(neighborNode))
	                {
	                    openList.Push(neighborNode);
	                }
                }
            }			
            closedList.Push(node);
            openList.Remove(node);
        }

        //We handle the scenario where no goal was found after looping thorugh the open list
        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }

        //Calculate the path based on the final node
        return CalculatePath(node);
    }
}
