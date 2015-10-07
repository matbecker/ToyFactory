using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(cameraFollow))]

public class cameraFollowEditor : Editor {

	public override void OnInspectorGUI() {
		//draw regular inspector
		DrawDefaultInspector ();

		cameraFollow cf = (cameraFollow)target;
		//draw button to set the min cam pos to the current camera position 
		if (GUILayout.Button("Set Min Cam Pos"))
		{
			//call min cam pos method
			cf.setMinCamPos();
		}
		//draw button to set the max cam pos to the current camera position
		if (GUILayout.Button("Set Max Cam Pos"))
		{
			//call max cam pos method
			cf.setMaxCamPos();
		}
	}
}
