using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {
	public GameObject player;
	public WayPoint[] wayPoints;
	public float attackRange;
	public Transform head;
	public float rotationSpeed = 5f;
	public float minimumRunSpeed = 0.01f;
	public float speed = 3f;
	public float followSpeed = 6f;
	public float radiusOfSight;
	public float angleOfSight;
	private int nextPointIndex = 0;
	private float EPSILON = 0.2f;
	private bool following = false;
	
		
	public FireMachineGunEnemy fire;

	// Use this for initialization
	void  Start () {
		
	}
	
	void FixedUpdate () 
	{
		
		CharacterSight.DrawCone(gameObject, radiusOfSight, angleOfSight, Color.blue);
		if (CharacterSight.IsEnemyInsideCone(gameObject, player, radiusOfSight, angleOfSight) || following) {
			//atack
			//Debug.Log("detecto jugador y hay que atacar");
			Follow();
		} else {
			Patrol();
		}
	}
	
	void Patrol () 
	{		
		// Move towards our target
		MoveTowards(nextPointIndex);
		
	}
	
	void MoveTowards(int index) {
		Vector3 direction = wayPoints[index].transform.position - gameObject.transform.position;
		
		// Rotate towards the target
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		CharacterController cc = GetComponent<CharacterController>();
		cc.SimpleMove(direction*speed);

		if (cc.velocity.magnitude < minimumRunSpeed) {
			animation.CrossFade("idle1");
			
		} else {
			
			animation.CrossFade("run");
		}
		
	}
	
	void Follow() {
		Vector3 direction = player.transform.position - transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		CharacterController cc = GetComponent<CharacterController>();
		cc.SimpleMove(direction*followSpeed);
		animation.CrossFade("run");
		following = true;
		fire.enabled = true;
	}
	
	
	// version basica
	bool CanSeeTarget() {
		if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange) {
			//Debug.Log("me fui por estar afuera del rango de ataque");
			return false;
		}
		
		Ray ray = new Ray(head.position, head.up);		
		
		RaycastHit hit = new RaycastHit();
		// lanzo un rayo
		
		if (Physics.Raycast(ray, out hit, 10.0f)) {
			//Debug.Log("choca");
            //stores the object hit
            Collider collider1 = hit.collider;
			if (collider1.name.Equals("Player")) 
			{
//				Debug.Log("attack");
				following = true;
				return true;
			} else {				
				//  oclusion
				return false;
			}
        }
		
		return false;
	}
	
	
	
	void OnTriggerEnter (Collider otherObject)
	{		
		if (otherObject.tag == "wayPointTag") {
			if (nextPointIndex == (wayPoints.Length - 1))
			{
				nextPointIndex = 0;
			} else
			{
				nextPointIndex++;	
			}
		}
		
		
		
	}
	
	
}
