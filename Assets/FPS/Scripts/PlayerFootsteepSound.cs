using UnityEngine;
using System.Collections;

public class PlayerFootsteepSound : MonoBehaviour {

	public AudioSource Foot1;
	public AudioSource Foot2;
	public AudioSource Foot3;
	public AudioSource Foot4;

	// Update is called once per frame
	void Update () {
		if(Input.GetAxis ("Vertical") > 0)
		{
			if (Foot1) {
				if (!Foot1.isPlaying)
					Foot1.Play();
			}
		}else{
			if(Input.GetAxis ("Vertical") < 0)
			{
				if (Foot2) {
					if (!Foot2.isPlaying)
						Foot2.Play();
				}
			}
		}


		if(Input.GetAxis ("Horizontal") > 0)
		{
			if (Foot1) {
				if (!Foot3.isPlaying)
					Foot3.Play();
			}
		}else{
			if(Input.GetAxis ("Horizontal") < 0)
			{
				if (Foot2) {
					if (!Foot4.isPlaying)
						Foot4.Play();
				}
			}
		}		
	}
}
