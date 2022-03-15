using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MonoBehaviour
{
	public PlayerWeek6 player;
	public List<WheelWeek6> wheels;

	public float acceleratingForce;
	public float brakingForce;
	public float reversingForce;
	public float maxWheelRotation;
	public float lateralFrictionCoefficient;
	
	public bool active;
	public bool subscribed;
	
	public enum DrivingModes
	{
		Neutral, Drive, Reverse
	}

	public enum SteeringModes
	{
		Neutral, Left, Right
	}
	
	public DrivingModes drivingMode;
	public SteeringModes steeringMode;

	public void ChangeDriveModes(DrivingModes mode)
	{
		drivingMode = mode;
	}
	
	public void ChangeSteerModes(SteeringModes mode)
	{
		steeringMode = mode;
	}

	public void DeactivateVehicle()
	{
		player.DriveEvent -= ChangeDriveModes;
		player.SteerEvent -= ChangeSteerModes;
		player.DeactivateVehicleEvent -= DeactivateVehicle;
		active = false;
		subscribed = false;
	}
	
    // Start is called before the first frame update
    public void BaseStart()
    {
	    foreach (var t in wheels)
	    {
		    if (t.canDrive)
		    {
			    t.acceleratingForce = acceleratingForce;
			    t.brakingForce = brakingForce;
			    t.reversingForce = reversingForce;
		    }

		    if (t.canSteer)
		    {
			    t.maxRotation = maxWheelRotation;
		    }
		    t.lateralFrictionCoefficient = lateralFrictionCoefficient;
	    }
    }

    // Update is called once per frame
    public void BaseUpdate()
    {
	    if (active)
	    {
		    if (!subscribed)
		    {
			    player.DeactivateVehicleEvent += DeactivateVehicle;
			    player.DriveEvent += ChangeDriveModes;
			    player.SteerEvent += ChangeSteerModes;

			    subscribed = true;
		    }
	    }
    }
}
