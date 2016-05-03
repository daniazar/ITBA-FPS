using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin skin;
	private string instructionText = "Instructions:\nPress the arrows to move.\nPress Spacebar to jump, P to pause.\nUse the mouse to aim and shot.\nTo win you need to reach the helicopter at the X.\nYou can increase your score by killing enemys or seagulls.";
	private int buttonWidth = 200;
	private int buttonHeight = 50;
	private int groupWidth = 300;
	private int groupHeight = 105;
	public Texture backgroundTexture;

	
	void OnGUI()
	{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height ), backgroundTexture);	
	
			GUI.Label(new Rect(Screen.width / 2 - groupWidth / 2 + 10, 10, 300, 200 ), instructionText);
			GUI.Box (new Rect (Screen.width / 2 - groupWidth / 2, 5 , groupWidth, groupHeight), "", GUI.skin.GetStyle("box"));
	        GUI.Label (new Rect (Screen.width / 2 - 90, Screen.height/2 , groupWidth, groupHeight), "Lonely island", skin.GetStyle("LoseSkin"));

			if(GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2  +90, buttonWidth, buttonHeight), "Start Game"))
			{
				Application.LoadLevel("Islands");
			}
    	
	}	
}
