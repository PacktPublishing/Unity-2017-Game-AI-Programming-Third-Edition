using UnityEngine;
using System.Collections.Generic;

public class FlockController : MonoBehaviour
{
    // The number of boids in the flock
    [SerializeField]
	private int flockSize = 20;

    // Speed modifer for the boid movement
    [SerializeField]
    private float speedModifier = 5;

    // Weight modifier for alignment value's contributionto the flocking direction.
    [SerializeField]
	private float alignmentWeight = 1;

    // Weight modifier for cohesion value's contributionto the flocking direction.
    [SerializeField]
	private float cohesionWeight = 1;

    // Weight modifier for separation value's contributionto the flocking direction.
    [SerializeField]
	private float separationWeight = 1;

    // Weight modifier for the target's contributionto the flocking direction.
    [SerializeField]
    private float followWeight = 5;
	
    [Header("Boid Data")]
    [SerializeField]
	private Boid prefab;
    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [Header("Target Data")]
    [SerializeField]
    public Transform target;
	
    //used to calculate the average center of the entire flock. Used in calculating cohesion.
	private Vector3 flockCenter;

    //Used to calculate the entire flock's direction. Used in calculating alignment.
	private Vector3 flockDirection;

    //The direction to the flocking target.
    private Vector3 targetDirection;

    //Separation value
    private Vector3 separation;

    public List<Boid> flockList = new List<Boid>();

    public float SpeedModifier { get { return speedModifier; }}

	private void Awake()
	{
        flockList = new List<Boid>(flockSize);     
		for (int i = 0; i < flockSize; i++)
		{
            //To avoid weird artifacts, we try to spawn the boids within radius rather than in the same position.
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
			Boid boid = Instantiate(prefab, spawnLocation, transform.rotation) as Boid;

            boid.transform.parent = transform;
            boid.FlockController = this;
            flockList.Add(boid);
		}
	}

    public Vector3 Flock(Boid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        flockDirection = Vector3.zero;
        flockCenter = Vector3.zero;
        targetDirection = Vector3.zero;
        separation = Vector3.zero;

        for (int i = 0; i < flockList.Count; ++i) 
        {
            Boid neighbor = flockList[i];
            //Check only against neighbors.
            if (neighbor != boid) 
            {
                //Aggregate the direction of all the boids.
                flockDirection += neighbor.Direction;
                //Aggregate the position of all the boids.
                flockCenter += neighbor.transform.localPosition;
                //Aggregate the delta to all the boids.
                separation += neighbor.transform.localPosition - boidPosition;
                separation *= -1;
            }
        }
        //Alignment. The avereage direction of all boids.
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        //Cohesion. The centroid of the flock.
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        //Separation.
        separation /= flockSize;
        separation = separation.normalized * separationWeight;

        //Direction vector to the target of the flock.
        targetDirection = target.localPosition - boidPosition;
        targetDirection = targetDirection * followWeight;

        return flockDirection + flockCenter + separation + targetDirection;
    }
}