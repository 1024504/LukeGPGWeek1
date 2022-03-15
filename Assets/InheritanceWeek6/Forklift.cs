using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : VehicleBase
{
	// Start is called before the first frame update
	void Start()
	{
		base.BaseStart();
	}
	
	// Update is called once per frame
	void Update()
	{
		base.BaseUpdate();
		//Debug.DrawRay(transform.position, GetComponent<Rigidbody>().velocity, Color.yellow);
	}
}
