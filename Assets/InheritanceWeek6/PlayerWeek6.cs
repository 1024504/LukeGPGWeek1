using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeek6 : MonoBehaviour
{
	public Camera Camera;
	
	public delegate void DriveAction(VehicleBase.DrivingModes mode);
	public event DriveAction DriveEvent;
		
	public delegate void SteerAction(VehicleBase.SteeringModes mode);
	public event SteerAction SteerEvent;

	public delegate void DeactivateVehicleAction();
	public event DeactivateVehicleAction DeactivateVehicleEvent;

	public delegate void ResetAction();
	public event ResetAction ResetEvent;

	void Start()
    {
	    Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
	    if( Input.GetMouseButtonDown(0) )
	    {
		    
		    
		    
		    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		    RaycastHit hit;
		    
		    if( Physics.Raycast( ray, out hit ) )
		    {
			    if (hit.transform.gameObject.GetComponent<VehicleBase>())
			    {
				    DeactivateVehicleEvent?.Invoke();
				    hit.transform.gameObject.GetComponent<VehicleBase>().player = this;
				    hit.transform.gameObject.GetComponent<VehicleBase>().ActivateVehicle();
				    hit.transform.gameObject.GetComponent<VehicleBase>().Camera.enabled = true;
				    Camera.enabled = false;
			    }
			    else
			    {
				    DeactivateVehicleEvent?.Invoke();
				    Camera.enabled = true;
			    }
		    }
		    else
		    {
			    DeactivateVehicleEvent?.Invoke();
			    Camera.enabled = true;
		    }
	    }
	    
	    if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
	    {
		    DriveEvent?.Invoke(VehicleBase.DrivingModes.Drive);
	    }
	    else if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
	    {
		    DriveEvent?.Invoke(VehicleBase.DrivingModes.Reverse);
	    }
	    else
	    {
		    DriveEvent?.Invoke(VehicleBase.DrivingModes.Neutral);
	    }

	    if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
	    {
		    SteerEvent?.Invoke(VehicleBase.SteeringModes.Left);
	    }
	    else if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
	    {
		    SteerEvent?.Invoke(VehicleBase.SteeringModes.Right);
	    }
	    else
	    {
		    SteerEvent?.Invoke(VehicleBase.SteeringModes.Neutral);
	    }

	    if (Input.GetKeyDown(KeyCode.R))
	    {
		    ResetEvent?.Invoke();
	    }
    }
}
