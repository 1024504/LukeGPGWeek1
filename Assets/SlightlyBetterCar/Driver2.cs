using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver2 : MonoBehaviour
{
	public GameObject car;
	private Rigidbody chassis;
	public float acceleratingForce = 5f;
	public float brakingForce = 10f;
	public float reversingForce = 3f;
	public float turningTorque = 3f;
	

	// Start is called before the first frame update
    void Start()
    {
	    chassis = car.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
	    {
		    if (Vector3.Dot(chassis.velocity,car.transform.TransformDirection(Vector3.forward)) < 0)
		    {
			    chassis.AddRelativeForce(new Vector3(0, 0, brakingForce));
		    }
		    else
		    {
			    chassis.AddRelativeForce(new Vector3(0, 0, acceleratingForce));
		    }
	    }
	    
	    if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
	    {
		    if (Vector3.Dot(chassis.velocity,car.transform.TransformDirection(Vector3.forward)) > 0)
		    {
			    chassis.AddRelativeForce(new Vector3(0, 0, -brakingForce));
		    }
		    else
		    {
			    chassis.AddRelativeForce(new Vector3(0, 0, -reversingForce));
		    }
	    }

	    if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
	    {
		    chassis.AddRelativeTorque(new Vector3 (0,-turningTorque,0));
	    }
	    
	    if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
	    {
		    chassis.AddRelativeTorque(new Vector3(0, turningTorque, 0));
	    }
    }
}
