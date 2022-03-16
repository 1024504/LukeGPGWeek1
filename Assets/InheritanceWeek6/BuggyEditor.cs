using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Buggy))]
public class BuggyEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Activate"))
		{
			Camera.main.enabled = false;
			Buggy buggy = (Buggy) target;
			//buggy.ActivateVehicle();
		}
	}
}
