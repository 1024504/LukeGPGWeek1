using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver2 : MonoBehaviour
{
	public GameObject car;

	// Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
	    {
		    
		    car.GetComponentInChildren<Rigidbody>().AddRelativeForce(new Vector3 (5,0,0));
	    }

	    if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
	    {
		    car.GetComponentInChildren<Rigidbody>().AddRelativeTorque(new Vector3 (0,-5,0));
	    }
	    
	    if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
	    {
		    car.GetComponentInChildren<Rigidbody>().AddRelativeTorque(new Vector3(0, 5, 0));
	    }
    }
}
