using UnityEngine;

public class Pathing : MonoBehaviour 
{
    [SerializeField]
    private Path path;
    [SerializeField]
    private float speed = 20.0f;
    [SerializeField]
    private float mass = 5.0f;
    [SerializeField]
    private bool isLooping = true;
    
    private float currentSpeed;
    private int currentPathIndex = 0;
    private Vector3 targetPoint;
    private Vector3 direction;
    private Vector3 targetDirection;  

	private void Start () 
    {
        // Initialize the direction as the agent's current facing direction
        direction = transform.forward; 
        // We get the firt point along the path
        targetPoint = path.GetPoint(currentPathIndex);
	}
	
	private void Update ()
    {
        if(path == null) {
            return;
        }

        currentSpeed = speed * Time.deltaTime;
        
        if(TargetReached())
        {
            if (!SetNextTarget()) {
                return;
            }
        }

        direction += Steer(targetPoint);
        transform.position += direction; //Move the agent according to the direction
        transform.rotation = Quaternion.LookRotation(direction); //Rotate the agent towards the desired direction
	}

    /*
     * Attempt to set the next target point. If there are enough points available,
     * we simply increment the count. If we're out of points we have two choices:
     * if the isLooping bool is true, we go back to the first point in the path, otherwise,
     * we return false, indicating that there are no more points to visit. */
    private bool SetNextTarget() 
    {
        bool success = false;
        if (currentPathIndex < path.PathLength - 1) {
            currentPathIndex++;
            success = true;
        } 
        else 
        {
            if(isLooping) 
            {
                currentPathIndex = 0;
                success = true;
            } 
            else 
            {
                success = false;
            }
        }
        targetPoint = path.GetPoint(currentPathIndex);
        return success;
    }

    /* We use the path's tolerence radius to determine if the agent is "close enough"
     * to the target point to consider it "reached" */
    private bool TargetReached() 
    {
        return (Vector3.Distance(transform.position, targetPoint) < path.radius);
    }

    /* Steering algorithm to steer the agent towards the target vector */
    public Vector3 Steer(Vector3 target)
    {
        // Subtracting vector b - a gives you the direction from a to b. 
        targetDirection = (target - transform.position);
        targetDirection.Normalize();        
        targetDirection*= currentSpeed;
		
        Vector3 steeringForce = targetDirection - direction; 
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }
}