using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

	private static int live;
	public GameObject player;
	public float WaterDeathDelay = 0.3f;
	public float BloodDelay = 0.2f;
	public float DieTime = 3f;
	private float timerWater;
	private float timerBlood;
	public Renderer Blood;
	//public GameObject ExplosionPrefab;
	public AudioSource DeathSound;
	public AudioSource HitSound;
	public static AudioSource HitSoun;

	public float WaterLevel = 30.0f;
	
	
	private static bool hit;
	void Start(){
		live = 100;
		hit = false;
		HitSoun = HitSound;

	}
	
	void Update () {
		UnderWater();
				if(live <= 0 )
			Die();
		

	}
	
	public static void getShot() 
	{
		live -=10;
			hit = true;
			if (HitSoun) {
				if (!HitSoun.isPlaying)
					HitSoun.Play();
			}
	}


		void LateUpdate() {
			if ( Blood) {
			// We were hit this frame, enable the blood
				if (hit) {
					Blood.transform.localRotation = Quaternion.AngleAxis(Random.value * 360, Vector3.up);
					Blood.enabled = true;
					hit = false;
					timerBlood = Time.time ;
				}else{
					if((Time.time - timerBlood)  > BloodDelay){
						Blood.enabled = false;
					}
				}
			}
		}
	
	void UnderWater(){
		if (player.transform.position.y <= WaterLevel) {
			if((Time.time - timerWater)  > WaterDeathDelay)
			{
				timerWater =  Time.time;
				live -=10;
				hit = true;
				if (HitSound) {
					if (!HitSound.isPlaying)
						HitSound.Play();
				}
	
			}
			
		}
	}


	void Die(){
		
			Application.LoadLevel("Lose");
		
	}


	
	
	
		void OnGUI ()
	{
		GUI.Label (new Rect (10, 30, 120, 20), "Live: " + live.ToString () + "%");
	}
	
}