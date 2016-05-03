using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {


	public FireMachineGun machineGun;

	void OnGUI ()
	{
		GUI.Label (new Rect (10, 50, 120, 20), "Bullets: " + machineGun.GetBulletsLeft());
		GUI.Label (new Rect (10, 70, 120, 20), "Clips: " + machineGun.GetClipsLeft());
		
	}

}
