using UnityEngine;

public class Avoidance : MonoBehaviour 
{
    [SerializeField]
    private float movementSpeed = 20.0f;
    [SerializeField]
    private float rotationSpeed = 5.0f;
    [SerializeField]
    private float force = 50.0f;
    [SerializeField]
    private float minimumAvoidanceDistance = 20.0f;
    [SerializeField]
    private float toleranceRadius = 3.0f;
    
    private float currentSpeed;
    private Vector3 targetPoint;
    private RaycastHit mouseHit;
    private Camera mainCamera;
    private Vector3 direction;
    private Quaternion targetRotation;
    private RaycastHit avoidanceHit;
    private Vector3 hitNormal;
    
	private void Start () 
    {
        mainCamera = Camera.main;
        targetPoint = Vector3.zero;
	}
    
	private void Update () 
    {
        CheckInput();
        direction = (targetPoint - transform.position);
        direction.Normalize();

        //Apply obstacle avoidance
        ApplyAvoidance(ref direction);

        //Don't move the agent when the target point is reached
        if(Vector3.Distance(targetPoint, transform.position) < toleranceRadius) {
            return;
        }
        
        currentSpeed = movementSpeed * Time.deltaTime;

        //Rotate the agent towards its target direction 
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed *  Time.deltaTime);

        //Move the agent forard
        transform.position += transform.forward * currentSpeed;
    }

    private void CheckInput() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out mouseHit, 100.0f)) {
                targetPoint = mouseHit.point;
            }
        }
    }
    
    private void ApplyAvoidance(ref Vector3 direction)
    {
        //Only detect layer 8 (Obstacles)
        //We use bitshifting to create a layermask with a value of 
        //0100000000 where only the 8th position is 1, so only it is active.
        int layerMask = 1 << 8;

        //Check that the agent hit with the obstacles within it's minimum distance to avoid
        if (Physics.Raycast(transform.position, transform.forward, out avoidanceHit, minimumAvoidanceDistance, layerMask))
        {
            //Get the normal of the hit point to calculate the new direction
            hitNormal = avoidanceHit.normal;
            hitNormal.y = 0.0f; //Don't want to move in Y-Space

            //Get the new direction vector by adding force to agent's current forward vector
            direction = transform.forward + hitNormal * force;
        }

    }
}