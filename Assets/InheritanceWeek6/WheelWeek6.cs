using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelWeek6 : MonoBehaviour
{
	public GameObject car;
	private Rigidbody chassis;
	private VehicleBase carClass;

	public bool canSteer;
	public bool canDrive;
	public Vector3 steerAngle;
	public float springCoefficient = 5f;
	public float dampingCoefficient = 1.5f;
	public float restingWheelHeight = 1f;
	public float wheelExtensionFactor = 1.5f;
	public bool isRearSteering;
	
	public float lateralFrictionCoefficient;
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
		if (Physics.Raycast(Ray, out RaycastHit, restingWheelHeight*wheelExtensionFactor))
		{
			chassis.AddForceAtPosition((springCoefficient * (restingWheelHeight - RaycastHit.distance)
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
			case VehicleBase.DrivingModes.Drive:
				if (Vector3.Dot(chassis.velocity, car.transform.TransformDirection(Vector3.forward)) < 0)
				{
					chassis.AddForceAtPosition((brakingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					if (canDrive)
					{
						chassis.AddForceAtPosition((acceleratingForce * car.transform.TransformDirection(Vector3.forward)), origin, 0);
					}
				}
				break;
			case VehicleBase.DrivingModes.Reverse:
				if (Vector3.Dot(chassis.velocity, car.transform.TransformDirection(Vector3.forward)) > 0)
				{
					chassis.AddForceAtPosition((-brakingForce*car.transform.TransformDirection(Vector3.forward)), origin, 0);
				}
				else
				{
					if (canDrive)
					{
						chassis.AddForceAtPosition((-reversingForce * car.transform.TransformDirection(Vector3.forward)), origin, 0);
					}
				}
				break;
		}
	}

	private void ApplySteering()
	{
		switch (carClass.steeringMode)
		{
			case VehicleBase.SteeringModes.Left:
				steerAngle = new Vector3 (0, -maxRotation, 0);
				break;
			case VehicleBase.SteeringModes.Right:
				steerAngle = new Vector3 (0, maxRotation, 0);
				break;
			case VehicleBase.SteeringModes.Neutral:
				steerAngle = Vector3.zero;
				break;
		}
	}

// Start is called before the first frame update
    void Awake()
    {
	    car = transform.parent.gameObject;
	    chassis = GetComponentInParent<Rigidbody>();
	    carClass = GetComponentInParent<VehicleBase>();
	    carClass.wheels.Add(this);
	    // should I even have this, or should I just set the bool in the prefab?
	    if (transform.InverseTransformDirection(transform.position).z < transform.InverseTransformDirection(transform.parent.position).z)
	    {
		    isRearSteering = true;
	    }
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
		    ApplyDriveForce();
		    
		    if (canSteer)
		    {
			    ApplySteering();
			    if (isRearSteering)
			    {
				    transform.localEulerAngles = -steerAngle;
			    }
			    else
			    {
				    transform.localEulerAngles = steerAngle;
			    }
		    }
		    
		    chassis.AddForceAtPosition(lateralFrictionCoefficient*chassis.mass*transform.TransformDirection(new Vector3 (-localVelocity.x, 0, 0)), origin);
		    
	    }
	}
}
