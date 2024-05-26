using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform t;
	public Vector3 leadPosition;
	private Vector3 _leadPrevPosition = Vector3.zero;
	
	[Range(0.01f,3f)]
	public float frequency = 1;
	[Range(0f,3f)]
	public float zeta = 1;
	[Range(-5f,5f)]
	public float r = 0;
	
	private Vector3 _nextPosition = Vector3.zero;
	private Vector3 _currentVelocity = Vector3.zero;
	private Vector3 _leadVelocity = Vector3.zero;

	private void OnEnable()
	{
		leadPosition = t.position;
	}

	private Vector3 NextStep(float timeStep, Vector3 x, Vector3 xd)
	{
		float constant1 = zeta / (Mathf.PI * frequency);
		float constant2Stable = Mathf.Max(1 / (2*Mathf.PI * frequency * 2*Mathf.PI * frequency), 1.1f * (timeStep*timeStep/4+timeStep*constant1/2));
		_nextPosition = t.position + timeStep * _currentVelocity;
		_currentVelocity += timeStep * (x + r *zeta / (2*Mathf.PI * frequency) * xd - _nextPosition - constant1 * _currentVelocity) / constant2Stable;
		return _nextPosition;
	}

	public void FixedUpdate()
	{
		t.position = NextStep(Time.fixedDeltaTime, leadPosition, _leadVelocity);
		_leadVelocity = (leadPosition - _leadPrevPosition) / Time.fixedDeltaTime;
		_leadPrevPosition = leadPosition;
	}
}
