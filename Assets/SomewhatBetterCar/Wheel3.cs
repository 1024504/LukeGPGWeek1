using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel3 : MonoBehaviour
{
	public GameObject car;
	private Rigidbody chassis;
	private Car3 carClass;

	public bool canSteer;
	public bool canDrive;
	public Vector3 steerAngle;
	public float springCoefficient = 5f;
	public float dampingCoefficient = 1.5f;
	public float maxDistance = 1f;
	
	public float lateralFriction;
	public float acceleratingForce;
	public float brakingForce;
	public float reversingForce;
	public float maxRotation;


	public Vector3 localVelocity;
	public Vector3 origin;
	public Vector3 direction;
	private Ray Ray;
	private RaycastHit RaycastHit;

	private bool CheckGroundContact()
	{
		Debug.DrawRay(Ray.origin, Ray.direction, Color.green);
		if (Physics.Raycast(Ray, out RaycastHit, maxDistance))
		{
			chassis.AddForceAtPosition((springCoefficient * (maxDistance - RaycastHit.distance)
			                            - dampingCoefficient * Vector3.Dot(chassis.velocity,
				                            car.transform.TransformDirection(Vector3.up)))
			                           * transform.TransformDirection(Vector3.up), origin, 0);
			return true;
		}
		return false;
	}

	private void ApplyDriveForce()
	{
		switch (carClass.drivingMode) 
		{
			case Car3.DrivingModes.Drive:
				if (Vector3.Dot(chassis.velocity, car.transform.TransformDirection(Vector3.forward)) < 0)
				{
					chassis.AddForceAtPosition((brakingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					chassis.AddForceAtPosition((acceleratingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				break;
			case Car3.DrivingModes.Reverse:
				if (Vector3.Dot(chassis.velocity, car.transform.TransformDirection(Vector3.forward)) > 0)
				{
					chassis.AddForceAtPosition((-brakingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					chassis.AddForceAtPosition((-reversingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				break;
		}
	}

	private void ApplySteering()
	{
		switch (carClass.turningMode)
		{
			case Car3.TurningModes.Left:
				transform.localEulerAngles = new Vector3 (0, -maxRotation, 0);
				break;
			case Car3.TurningModes.Right:
				transform.localEulerAngles = new Vector3 (0, maxRotation, 0);
				break;
			case Car3.TurningModes.Neutral:
				transform.localEulerAngles = new Vector3 (0, 0, 0);
				break;
		}
	}

// Start is called before the first frame update
    void Start()
    {
	    chassis = car.GetComponent<Rigidbody>();
	    carClass = car.GetComponent<Car3>();
    }

    // Update is called once per frame
    void Update()
    {
	    origin = transform.position;
	    direction = transform.TransformDirection(Vector3.down);
	    Ray = new Ray(origin, direction);
	    localVelocity = transform.InverseTransformDirection(chassis.velocity);

	    if (CheckGroundContact())
	    {
		    if (canDrive)
		    {
			    ApplyDriveForce();
		    }
		    
		    if (canSteer)
		    {
			    ApplySteering();
		    }
		    
		    chassis.AddForceAtPosition(transform.TransformDirection(new Vector3 (-Mathf.Clamp(
			    Vector3.Dot(localVelocity, transform.TransformDirection(Vector3.left)) ,-lateralFriction, 
			    lateralFriction), 0, 0)), origin);
	    }
	}
}
