using UnityEngine;
using System.Collections;

public static class helperFunctions {
	
	public static float Vector2ToDirection(Vector2 dir)
	{
		
		float theAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		
		return theAngle;

	}
}
