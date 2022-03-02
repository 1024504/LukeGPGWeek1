using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel2 : MonoBehaviour
{
	public GameObject car;
	private Rigidbody chassis;
	public float springCoefficient = 5f;
	public float dampingCoefficient = 0.5f;
	public float maxDistance = 1f;
	public Vector3 origin;
	public Vector3 direction;
	private Ray Ray;
	private RaycastHit RaycastHit;


	// Start is called before the first frame update
    void Start()
    {
	    chassis = car.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	    origin = transform.position;
	    direction = transform.TransformDirection(Vector3.down);
	    Ray = new Ray(origin, direction);
	    
	    Debug.DrawRay(Ray.origin, Ray.direction, Color.green);
	    if (Physics.Raycast(Ray, out RaycastHit, maxDistance))
	    {
		    chassis.AddForceAtPosition((springCoefficient*(maxDistance-RaycastHit.distance)
		                                -dampingCoefficient*Vector3.Dot(chassis.velocity,car.transform.TransformDirection(Vector3.up)))
		                               *transform.TransformDirection(Vector3.up),origin,0);
	    }
    }
}
