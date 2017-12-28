using UnityEngine;

public class Path: MonoBehaviour
{
    [SerializeField]
    private Vector3[] waypoints;

    public bool isDebug = true;
    public float radius = 2.0f;

    public float PathLength {
        get { return waypoints.Length; }
    }
    
    public Vector3 GetPoint(int index)
    {
        return waypoints[index];
    }
    
    private void OnDrawGizmos()
    {
        if (!isDebug) {
            return;
        }

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i + 1 < waypoints.Length)
            {
                Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red);
            }
        }
    }
}