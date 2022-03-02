using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car3 : MonoBehaviour
{
	public Vector3 localVelocity;
	public List<GameObject> wheels;

	public float acceleratingForce = 0.5f;
	public float brakingForce = 10f;
	public float reversingForce = 3f;
	public float maxWheelRotation = 45f;
	public float lateralFriction = 8f;
	
	public enum DrivingModes
	{
		Drive, Neutral, Reverse
	}

	public enum TurningModes
	{
		Left, Neutral, Right
	}
	
	public DrivingModes drivingMode;
	public TurningModes turningMode;
	
	// Start is called before the first frame update
	void Start()
	{
		foreach (var t in wheels)
		{
			if (t.GetComponent<Wheel3>().canDrive)
			{
				t.GetComponent<Wheel3>().acceleratingForce = acceleratingForce;
				t.GetComponent<Wheel3>().brakingForce = brakingForce;
				t.GetComponent<Wheel3>().reversingForce = reversingForce;
			}

			if (t.GetComponent<Wheel3>().canSteer)
			{
				t.GetComponent<Wheel3>().maxRotation = maxWheelRotation;
			}
			t.GetComponent<Wheel3>().lateralFriction = lateralFriction;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		Debug.DrawRay(transform.position, GetComponent<Rigidbody>().velocity, Color.yellow);
	}
}
