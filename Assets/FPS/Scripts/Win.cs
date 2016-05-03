using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	void OnTriggerEnter (Collider otherObject)
	{
		//Debug.Log("Hit an " + otherObject.name);
		if (otherObject.tag == "Player" ) {
			Application.LoadLevel("Win");
		}
	}
	
	void OnTriggerStay (Collider otherObject)
	{
		//Debug.Log("Hit an " + otherObject.name);
		if (otherObject.tag == "Player" ) {
			Application.LoadLevel("Win");
		}
	}
	
}