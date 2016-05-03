using UnityEngine;
using System.Collections;

public class SeagullDeath : MonoBehaviour {


		void OnTriggerEnter(Collider otherObject)
	{
		
		
		//EnemyManager em = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();
		if (otherObject.name == "Terrain")
		{
			if(rigidbody.useGravity ){
				rigidbody.useGravity = false;
				transform.position =  new Vector3(0 , 100 , 0);
				Score.KillSeagull();
			}
		}
	}

}
