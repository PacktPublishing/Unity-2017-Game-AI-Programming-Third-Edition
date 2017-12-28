using UnityEngine;
using System;

public class Node : IComparable
{
    //Total cost so far for the node
    public float gCost;
    //Estimated cost from this node to the goal node
    public float hCost;
    //Is this an obstacle node
    public bool bObstacle;
    //Parent of the node in the linked list
    public Node parent;
    //Position of the node in world space
    public Vector3 position;            
    
    public Node()
    {
        hCost = 0.0f;
        gCost = 1.0f;
        bObstacle = false;
        parent = null;
    }
    
    public Node(Vector3 pos)
    {
        hCost = 0.0f;
        gCost = 1.0f;
        bObstacle = false;
        parent = null;

        position = pos;
    }
    
    public void MarkAsObstacle()
    {
        bObstacle = true;
    }
    
    //IComparable Interface method implementation
    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        if (hCost < node.hCost) 
        {
            return -1;
        }
        if (hCost > node.hCost) 
        {
            return 1;
        }
        return 0;
    }
}


