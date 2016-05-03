using UnityEngine;
using System.Collections;

public class LoseWin : MonoBehaviour {

	public Texture backgroundTexture;
	public float maxTime = 4;
	private float time =0;
	public GUISkin skin;

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height ), backgroundTexture);	
		GUI.Label(new Rect(1, 5, 240, 20), "Your Score: " + Score.getScore().ToString(), skin.GetStyle("LoseSkin"));
		time += Time.deltaTime;

		if(Input.anyKeyDown  && time > maxTime)
		{
			
				Application.LoadLevel("Islands");
		}
	}
		
	
}