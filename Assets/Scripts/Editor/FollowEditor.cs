using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Follow)), CanEditMultipleObjects]
public class FollowEditor : Editor
{
	// public void OnSceneGUI()
	// {
	// 	Follow follow = (Follow)target;
	// 	
	// 	EditorGUI.BeginChangeCheck();
	// 	Vector3 newLeadLocalPosition = Handles.PositionHandle(follow.transform.TransformPoint(follow.leadLocalPosition), Quaternion.identity);
	// 	if (EditorGUI.EndChangeCheck())
	// 	{
	// 		Undo.RecordObject(follow, "Change Look At Target Position");
	// 		follow.FixedUpdate();
	// 		follow.leadLocalPosition = newLeadLocalPosition;
	// 	}
	// }
}
