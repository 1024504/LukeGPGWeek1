using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
	public static Vector3 BezierFunction(List<Vector3> controlPoints, float t) 
	{
		List<Vector3> a = controlPoints;
		List<Vector3> b = new List<Vector3>(controlPoints.Count-1);
		
		for (var i = 0; i < controlPoints.Count-1; i++)
		{
			for (var j = 0; j < b.Capacity; j++)
			{
				b.Add(Vector3.Lerp(a[j], a[j + 1], t));
			}

			a = b;
			if (a.Count - 1 != 0)
			{
				b = new List<Vector3>(a.Count - 1);
			}
		}

		//Debug.Log(a[0]);
		return a[0];
	}
}
