using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	private float live;
	public float WaterDeathDelay = 0.3f;
	public float DieTime = 3f;
	private float timerWater;
	//public GameObject ExplosionPrefab;
	public AudioSource DeathSound;
	public AudioSource HitSound;
	public float WaterLevel = 30.0f;
	
	public FireMachineGunEnemy fire;
	public IA ia;
	private bool dead = false;
	void Start(){
		live = 100.0f;
	}
	
	void Update () {
		UnderWater();
				if(live <= 0 )
					Die();
		

	}


	
	
	void UnderWater(){
		if (transform.position.y <= WaterLevel) {
			if((Time.time - timerWater)  > WaterDeathDelay)
			{
				timerWater =  Time.time;
				live -=10;
				if (HitSound) {
					if (!HitSound.isPlaying)
						HitSound.Play();
				}
	
			}
			
		}
	}
	
	
	public void GetHit(RaycastHit hit, float damage){
		//we only have a machinegun so there is really only one damage...
		live -= damage;
		
	}


	void Die(){
		if(dead == false){
			ia.enabled = false;
			fire.enabled = false;
			animation.CrossFade("die");
			Score.KillEnemy();
			dead = true;
			StartCoroutine (Death());
			
			}
	}

	IEnumerator Death ()
	{
	//	Instantiate (ExplosionPrefab, transform.position, transform.rotation);
		if (DeathSound) {
				if (!DeathSound.isPlaying)
					DeathSound.Play();
		}
		yield return new WaitForSeconds (DieTime);
		Destroy(gameObject);	
	}
	
	
}