using UnityEngine;
using System.Collections;

public class LightPulse : MonoBehaviour {


	public float MaxIntensity = 4;
	public float frecuency = 2;
	
	// Update is called once per frame
	void Update () {
	light.intensity = MaxIntensity * Mathf.Sin( frecuency * Time.time );

	}
}
