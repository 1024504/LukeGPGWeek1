using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public Vector3 localVelocity;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
	    //Debug.Log(localVelocity);
	    GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, 0, -(Mathf.Clamp((localVelocity.x),-8, 8))));
    }
}
