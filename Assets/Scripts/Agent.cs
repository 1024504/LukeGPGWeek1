using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Agent : MonoBehaviour
{
	public float cycleProgress;

	public float restingCycleProgress;
	public float restingFactor = 1;
	public bool isMoving = false;
	public Vector2 moveInput;

	public float cycleSpeed = 1;
	public float moveSpeed = 1;
	public float bodyHeight = 2;
	public float bodyF; 
	public float bodyZeta;
	public float bodyR;
	
	public float feetCyclePeakHeight = 1;
	public float feetF;
	public float feetZeta;
	public float feetR;

	public Transform t;
	
	public Follow body;

	public Follow foot1;
	private Transform _foot1Transform;
	private Vector3 _localFoot1Position;
	
	public Follow foot2;
	private Transform _foot2Transform;
	private Vector3 _localFoot2Position;
	
	public Follow foot3;
	private Transform _foot3Transform;
	private Vector3 _localFoot3Position;
	
	public Follow foot4;
	private Transform _foot4Transform;
	private Vector3 _localFoot4Position;

	private float CycleProgress
	{
		get => cycleProgress;
		set
		{
			cycleProgress = value;
			if (cycleProgress >= 4) cycleProgress -= 4;
			else if (cycleProgress < 0) cycleProgress += 4;
			restingCycleProgress = Mathf.Round(CycleProgress * restingFactor) / restingFactor;
			float cycleRes = Mathf.Sin(CycleProgress % 1 * 0.5f * Mathf.PI) * moveSpeed;
			float legHeight = -Mathf.Cos(CycleProgress % 1 * 2* Mathf.PI)*feetCyclePeakHeight+feetCyclePeakHeight/2;
			switch (cycleProgress)
			{
				case <= 1:
					foot1.leadPosition = body.leadPosition+_localFoot1Position+new Vector3(cycleRes * moveInput.x, legHeight, cycleRes * moveInput.y);
					break;
				case <= 2:
					foot2.leadPosition = body.leadPosition+_localFoot2Position+new Vector3(cycleRes * moveInput.x, legHeight, cycleRes * moveInput.y);
					break;
				case <= 3:
					foot3.leadPosition = body.leadPosition+_localFoot3Position+new Vector3(cycleRes * moveInput.x, legHeight, cycleRes * moveInput.y);
					break;
				case <= 4:
					foot4.leadPosition = body.leadPosition+_localFoot4Position+new Vector3(cycleRes * moveInput.x, legHeight, cycleRes * moveInput.y);
					break;
			}
		}
	}
	
	public void MovePerformed(Vector2 input)
	{
		isMoving = true;
		moveInput = input;
	}

	public void MoveCancelled()
	{
		isMoving = false;
		moveInput = Vector2.zero;
	}

	private void OnEnable()
	{
		t = transform;
		GetLegs();
		SetParameters();
	}

	private void FixedUpdate()
	{
		Move();
		GetBodyLead();
	}

	private void Move()
	{
		if (isMoving)
		{
			CycleProgress += Time.fixedDeltaTime * cycleSpeed * moveInput.magnitude;
		}
		else if (CycleProgress < restingCycleProgress)
		{
			CycleProgress += Time.fixedDeltaTime * cycleSpeed;
			if (CycleProgress > restingCycleProgress) CycleProgress = restingCycleProgress;
		}
		else if (CycleProgress > restingCycleProgress)
		{
			CycleProgress -= Time.fixedDeltaTime * cycleSpeed;
			if (CycleProgress < restingCycleProgress) CycleProgress = restingCycleProgress;
		}
	}
	
	private void GetLegs()
	{
		_foot1Transform = foot1.transform;
		_localFoot1Position = t.InverseTransformPoint(_foot1Transform.position); // might be wrong transform
		_foot2Transform = foot2.transform;
		_localFoot2Position = t.InverseTransformPoint(_foot2Transform.position);
		_foot3Transform = foot3.transform;
		_localFoot3Position = t.InverseTransformPoint(_foot3Transform.position);
		_foot4Transform = foot4.transform;
		_localFoot4Position = t.InverseTransformPoint(_foot4Transform.position);
	}

	private void GetBodyLead()
	{
		Vector3 position = _foot1Transform.position;
		Vector3 position1 = _foot2Transform.position;
		Vector3 position2 = _foot3Transform.position;
		Vector3 position3 = _foot4Transform.position;
		body.leadPosition = new Vector3(0,1,0) + (position + position1 + position2 + position3)/4;
		body.leadPosition = new Vector3((position.x + position1.x + position2.x + position3.x)/4,bodyHeight,(position.z + position1.z + position2.z + position3.z)/4);
	}

	private void SetParameters()
	{
		foot1.zeta = feetZeta;
		foot1.frequency = feetF;
		foot1.r = feetR;
		foot2.zeta = feetZeta;
		foot2.frequency = feetF;
		foot2.r = feetR;
		foot3.zeta = feetZeta;
		foot3.frequency = feetF;
		foot3.r = feetR;
		foot4.zeta = feetZeta;
		foot4.frequency = feetF;
		foot4.r = feetR;
		
		body.zeta = bodyZeta;
		body.frequency = bodyF;
		body.r = bodyR;
	}
}
