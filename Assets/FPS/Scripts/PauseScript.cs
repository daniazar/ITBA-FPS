using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	//Disable mosue look at....
	public MonoBehaviour scriptLookY;
	public MonoBehaviour scriptLookX;
	
	protected bool paused;
	private float oldScale = Time.timeScale;
	
	void OnPauseGame ()
	{
        paused = true;
	}

	void OnResumeGame ()
	{
        paused = false;
	}

	void Update ()
	{
        if (Input.GetButtonDown("pause")) {
			paused = !paused;
			if(paused)
			{
				PauseEnable();	
			}else{
				PauseDisable();
			}	
	    }
	}

	void PauseEnable(){
		oldScale = Time.timeScale;
		Time.timeScale = 0.0f;
		scriptLookX.enabled = false;
		scriptLookY.enabled = false;
	}	
	
	void PauseDisable(){
		Time.timeScale = oldScale;
		scriptLookX.enabled = true;
		scriptLookY.enabled = true;
	}
}