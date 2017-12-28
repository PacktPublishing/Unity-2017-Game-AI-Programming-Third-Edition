using System.Collections;

public class PriorityQueue 
{
    private ArrayList nodes = new ArrayList();
    
    public int Length
    {
        get { return nodes.Count; }
    }
    
    public bool Contains(object node)
    {
        return nodes.Contains(node);
    }
    
    public Node GetFirstNode()
    {
        if (nodes.Count > 0)
        {
            return (Node)nodes[0];
        }
        return null;
    }
    
    public void Push(Node node)
    {
        nodes.Add(node);
        nodes.Sort();
    }

    public void Remove(Node node)
    {
        nodes.Remove(node);
        nodes.Sort();
    }

}


