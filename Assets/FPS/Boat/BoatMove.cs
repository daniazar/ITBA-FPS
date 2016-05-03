using UnityEngine;
using System.Collections;

public class BoatMove : MonoBehaviour {
	public Collider col;
	public ConfigurableJoint configurableJoint;
	Vector3 vel = new Vector3( 0, 0 , 0.5f);
	private	bool stop = false;
	private int count = 0;
	Transform player;
	public float PlayerHeight = 40f;
	void Start () {
		stop = false;
		player = GameObject.FindWithTag("Player").transform;
	}

 	void FixedUpdate () {
		if(!stop)
		{
 			transform.Translate(vel);
 			player.position = new Vector3(transform.position.x, PlayerHeight, transform.position.z);
		}else
		{
			if(count < 5)
			{
				transform.Translate(new Vector3(0,0,3 ));
				player.position = new Vector3(transform.position.x, PlayerHeight, transform.position.z);
	
				count++;
			}
			if(count >= 5){
				Destroy(configurableJoint);			
				Destroy(rigidbody);
				Destroy(col);	
				Destroy(this);	
			}
			
		}
//		rigidbody.AddTorque(new Vector3( 0, 0, 500));
	}
	
	
	
	void OnTriggerEnter(Collider otherObject)
	{
		
//		Debug.Log(otherObject.name);
	
		//EnemyManager em = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();
		if ( otherObject.name == "Terrain")
		{
			stop = true;
		}
	}

}
