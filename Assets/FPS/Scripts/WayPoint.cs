using UnityEngine;
using System.Collections;

public class WayPoint: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos () {
		Gizmos.DrawIcon (transform.position, "Waypoint.tif");
	}
}
