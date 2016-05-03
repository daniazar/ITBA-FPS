using UnityEngine;
using System.Collections;

public class FireMachineGun : MonoBehaviour {


	public float range = 100.0f;
	public float fireRate = 0.05f;
	public float force = 10.0f;
	public float damage = 5.0f;
	public int bulletsPerClip = 40;
	public int clips = 20;
	public float reloadTime = 0.5f;
	public Renderer muzzleFlash;
	public ParticleEmitter hitParticles;
	public ParticleEmitter hitBlood;
	private int bulletsLeft = 0;
	private float nextFireTime = 0.0f;
	private bool shot = false;
	void Start () {

		//hitParticles = GetComponentInChildren(ParticleEmitter);
		// We don't want to emit particles all the time, only when we hit something.
		if (hitParticles)
			hitParticles.emit = false;
		if (hitBlood)
			hitBlood.emit = false;
		
		bulletsLeft = bulletsPerClip;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")){
			Fire();		
		}

	}
	
	void Fire () {
		if (bulletsLeft == 0){
			Reload();
			return;
		}
		// Keep firing until we used up the fire time
		if( nextFireTime < Time.time && bulletsLeft != 0) {
			FireOneShot();
			nextFireTime = Time.time + fireRate ;
		}
			
		// Reload gun in reload Time		
		if (bulletsLeft == 0 ){
			StartCoroutine (Reload());			
		}
	}

void LateUpdate() {
	if (muzzleFlash) {
		// We shot this frame, enable the muzzle flash
		if (shot) {
			muzzleFlash.transform.localRotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
			muzzleFlash.enabled = true;

			if (audio) {
				if (!audio.isPlaying)
					audio.Play();
				audio.loop = true;
			}
			shot = false;
		} else {
		// We didn't, disable the muzzle flash
			muzzleFlash.enabled = false;
			
			// Play sound
			if (audio)
			{
				audio.loop = false;
			}
		}
	}
}


	void FireOneShot () {
		Vector3 direction = Camera.main.transform.forward;
		RaycastHit hit;
		Vector3 pos = Camera.main.transform.position;
		// Did we hit anything?
		if (Physics.Raycast (pos, direction, out hit, range)) {
			// Apply a force to the rigidbody we hit
			//Debug.Log(hit.collider.gameObject.tag) ;
				
			if (hit.rigidbody)
			{
				hit.rigidbody.AddForceAtPosition(force * direction, hit.point);
				if(hit.collider.gameObject.tag == "Seagull3" || hit.collider.gameObject.tag == "Seagull1" || hit.collider.gameObject.tag == "Seagull2" || hit.collider.gameObject.tag == "Seagull4" )
				{
					/*Debug.Log(hit.collider.gameObject) ;*/
					hit.rigidbody.useGravity = true;
					DrawBlood(hit);	
				}else{
						DrawFire(hit);	
					}
						CheckEnemy(hit);
			}else{
				DrawFire(hit);
				CheckEnemy(hit);

				// Place the particle system for spawing out of place where we hit the surface!
				// And spawn a couple of particles
			}			
			
			
			//Debug.DrawLine(pos, hit.point, Color.red);
            //Debug.Log(hit.collider.name); // this will tell you what you are hitting
			// Send a damage message to the hit object			
			//hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		bulletsLeft--;
		// Register that we shot this frame,
		// so that the LateUpdate function enabled the muzzleflash renderer for one frame
		shot = true;
	}
	
	void DrawFire(RaycastHit hit){
		if (hitParticles) {
			hitParticles.transform.position = hit.point;
			hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			hitParticles.Emit();
		}
	}

	void CheckEnemy(RaycastHit hit){
			if(hit.collider){
					if(hit.collider.gameObject.tag == "Enemy"){
						DrawBlood(hit);
						((EnemyLife)hit.collider.gameObject.GetComponent("EnemyLife")).GetHit(hit, damage);	
					}
			}
	}

	void DrawBlood(RaycastHit hit){
		if (hitBlood) {
			hitBlood.transform.position = hit.point;
			hitBlood.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			hitBlood.Emit();
		}
	}
	IEnumerator Reload () {
		// Wait for reload time first - then add more bullets!
		yield return new WaitForSeconds(reloadTime);
		// We have a clip left reload
		if (clips > 0) {
			clips--;
			bulletsLeft = bulletsPerClip;
		}
	}

	public int GetBulletsLeft () {
		return bulletsLeft;
	}
	
	
	public int GetClipsLeft () {
		return clips;
	}
}
