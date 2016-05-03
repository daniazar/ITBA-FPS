// Mono Framework
using System;

// Unity Framework
using UnityEngine;

/// <summary>
/// Utility methods to calculate a basic sight for NPCs
/// </summary>
public class CharacterSight
{

	/// <summary>
	/// Check the sight from this gameObject to the specified gameObject
	/// Returns the gameObject that is in sight.
	/// </summary>
	public static GameObject IsEnemyAtSight(GameObject source, GameObject target, float radiusOfSight, float angleOfSight, bool drawGuideLines)
	{
		Vector3 posFrom = source.transform.position;
		
		// 1. Check the distance
        if (Vector3Util.DistanceXZ(target.transform.position, posFrom) < radiusOfSight)
        {
            // 2. Check if the ray is inside the detection cone
            Vector3 vecRay = target.transform.position - posFrom;
            Vector2 v1 = Vector2Util.Normalize(new Vector2(vecRay.x, vecRay.z));
            Vector2 v2 = new Vector2(source.transform.forward.x, source.transform.forward.z);

            float angle = 0.0f;

            if (v1 != v2)
                angle = Vector2Util.GetAngleBetweenVectors(v1, v2);


            if ((angleOfSight / -2.0f) <= angle && angle <= (angleOfSight / 2.0f))
            {

                // 3. Check the occlusion to the enemy
                RaycastHit hit;

                if (Physics.Raycast(posFrom, target.transform.position - posFrom, out hit))
                {
                    if (hit.transform.name == target.name)
                    {

                        if (drawGuideLines)
                            Debug.DrawRay(posFrom, target.transform.position - posFrom, Color.green);

                        return target;
                    }

                }

            }
        }
		
		if (drawGuideLines)
			Debug.DrawRay(posFrom, target.transform.position - posFrom, Color.red);
		
		
		return null;
	}
	
	/// <summary>
	/// Is the gameobject target inside the enemy cone from source
	/// </summary>
	public static bool IsEnemyInsideCone(GameObject source, GameObject target, float radiusOfSight, float angleOfSight)
	{
		Vector3 posFrom = source.transform.position;
		
//		Debug.Log(Vector3Util.DistanceXZ(target.transform.position, posFrom));
		// 1. Check the distance
		if (Vector3Util.DistanceXZ(target.transform.position, posFrom) < radiusOfSight)
		{
			// 2. Check if the ray is inside the detection cone
			Vector3 vecRay = target.transform.position - posFrom;
			Vector2 v1 = Vector2Util.Normalize(new Vector2(vecRay.x, vecRay.z));
			Vector2 v2 = new Vector2(source.transform.forward.x, source.transform.forward.z);
			
			float angle = 0.0f;
			
			if (v1 != v2)
				angle = Vector2Util.GetAngleBetweenVectors(v1, v2);
			
			
			if ((angleOfSight / -2.0f) <= angle && angle <= (angleOfSight / 2.0f))
			{
				return true;
			}
			
		}
		return false;
	}
	
	/// <summary>
	/// Draws the cone of sight on editor mode for testing purposes
	/// </summary>
	public static void DrawCone(GameObject go, float radiusOfSight, float angleOfSight, Color lineColor)
	{

		Vector3 pos = go.transform.position;
		
		// Local shape of the detection cone
		Vector3 x1l = new Vector3(radiusOfSight, 0, 0);
		Vector3 x2l = new Vector3(radiusOfSight * Mathf.Cos((angleOfSight / 2.0f) * Mathf.Deg2Rad), 0, radiusOfSight * Mathf.Sin((angleOfSight / 2.0f) * Mathf.Deg2Rad));
		Vector3 x3l = new Vector3(radiusOfSight * Mathf.Cos((angleOfSight / -2.0f) * Mathf.Deg2Rad), 0, radiusOfSight * Mathf.Sin((angleOfSight / -2.0f) * Mathf.Deg2Rad));
		
		// The facing angle of the character (in DEG)
		float rotAngle = go.transform.localEulerAngles.y - 90;
		
		// The transformed cone in local coords
		Vector3 x1t = Vector3Util.RotateY(x1l, rotAngle);
		Vector3 x2t = Vector3Util.RotateY(x2l, rotAngle);
		Vector3 x3t = Vector3Util.RotateY(x3l, rotAngle);
		
		// The final point in world position
	    Vector3 x0 = pos;
		Vector3 x1 = new Vector3(pos.x + x1t.x, pos.y + x1t.y, pos.z - x1t.z);
		Vector3 x2 = new Vector3(pos.x + x2t.x, pos.y + x2t.y, pos.z - x2t.z);
		Vector3 x3 = new Vector3(pos.x + x3t.x, pos.y + x3t.y, pos.z - x3t.z);
		
		// Draw the debug lines of the cone
		Debug.DrawLine(x0, x2, lineColor);
		Debug.DrawLine(x2, x1, lineColor);
		Debug.DrawLine(x1, x3, lineColor);
		Debug.DrawLine(x3, x0, lineColor);
	}
	
}
