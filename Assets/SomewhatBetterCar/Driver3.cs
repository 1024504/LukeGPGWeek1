using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver3 : MonoBehaviour
{
	public GameObject car;
	
	
	private Rigidbody chassis;
	private Car3 carClass;

	// Start is called before the first frame update
    void Start()
    {
	    chassis = car.GetComponentInChildren<Rigidbody>();
	    carClass = car.GetComponentInChildren<Car3>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
	    {
		    carClass.drivingMode = Car3.DrivingModes.Drive;
	    }
	    else if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
	    {
		    carClass.drivingMode = Car3.DrivingModes.Reverse;
	    }
	    else
	    {
		    carClass.drivingMode = Car3.DrivingModes.Neutral;
	    }

	    if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
	    {
		    carClass.turningMode = Car3.TurningModes.Left;

	    }
	    else if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
	    {
		    carClass.turningMode = Car3.TurningModes.Right;
	    }
	    else
	    {
		    carClass.turningMode = Car3.TurningModes.Neutral;
	    }
    }
}
