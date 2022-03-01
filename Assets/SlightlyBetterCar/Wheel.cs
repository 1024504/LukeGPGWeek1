using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	public GameObject car;
	public float stiffness = 0.1f;
	public float maxDistance = 1f;
	public Vector3 origin;
	public Vector3 direction;
	private Ray Ray;
	private RaycastHit RaycastHit;


	// Start is called before the first frame update
    void Start()
    {
	    
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
		    Debug.Log(RaycastHit.distance);
		    car.GetComponent<Rigidbody>().AddForceAtPosition((stiffness/RaycastHit.distance)*transform.TransformDirection(Vector3.up),origin,0);
	    }
    }
}
