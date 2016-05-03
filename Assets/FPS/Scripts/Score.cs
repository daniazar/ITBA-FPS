using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	private static int score =0;
	
	// Use this for initialization
	void Start () {
		score = 0;
	}

	public static void KillSeagull(){
		score += 100;
	}
	public static void KillEnemy(){
		score += 25;
	}


	void OnGUI ()
	{
		GUI.Label (new Rect (10, 10, 120, 20), "Score: " + score.ToString ());
//		GUI.Label (new Rect (10, 30, 60, 20), "Lives: " + PlayerController.lives.ToString ());
		
	}
	
	public static int getScore(){
		return score;
	}

}
