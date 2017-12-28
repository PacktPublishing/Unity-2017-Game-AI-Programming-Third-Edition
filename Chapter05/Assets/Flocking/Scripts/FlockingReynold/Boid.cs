using UnityEngine;

public class Boid : MonoBehaviour 
{
    [SerializeField]
	private FlockController flockController;
    
    //The modified direction for the boid.
    private Vector3 targetDirection;
    //The Boid's current direction.
    private Vector3 direction;

    public FlockController FlockController 
    {
        get { return flockController; }
        set { flockController = value; }
    }
    
    public Vector3 Direction { get { return direction; }}

    private void Awake() 
    {
        direction = transform.forward.normalized;
        if(flockController != null) 
        {
            Debug.LogError("You must assign a flock controller!");
        }
    }

    private void Update() {
        targetDirection = FlockController.Flock(this, transform.localPosition, direction);
        if(targetDirection == Vector3.zero)
        {
            return;
        }
        direction = targetDirection.normalized;
        direction *= flockController.SpeedModifier;
        transform.Translate(direction * Time.deltaTime);
    }	
}