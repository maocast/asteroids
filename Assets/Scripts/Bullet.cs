using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 10.0f;
	public float damage = 1.0f;
	public float timeToLive = 4.0f;
	
	public GameObject explosion;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		Invoke("kill", timeToLive);
	}
	
	// Update is called once per frame
	void Update () {
	}


 

	void OnCollisionEnter (Collision collision) {
		//Create explosion when object collides

		// this ain't working as expected 
		// bullets push each other around.

		if (collision.gameObject.CompareTag("Bullet")) {
			Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
		}

		//Collide against asteroid
		if(collision.gameObject.CompareTag("Asteroid"))
		{
			GameObject exp = (GameObject)GameObject.Instantiate (explosion, transform.position, transform.rotation);

			Asteroid asteroid = (Asteroid)collision.gameObject.GetComponent ("Asteroid");

			asteroid.applyDamage(damage);

			//Attach explosion to asteroid
			exp.transform.parent = asteroid.transform;
		}
		
		//Eliminate bullet
		if (!collision.gameObject.CompareTag("Bullet")) {
		  Destroy(gameObject);
		}

	}
	
	void kill () {
		GameObject.Destroy(this.gameObject);
	}
}
