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
		GameObject exp = (GameObject)GameObject.Instantiate (explosion, transform.position, transform.rotation);
		
		//Collide against asteroid
		if(collision.gameObject.CompareTag("Asteroid"))
		{
			Asteroid asteroid = (Asteroid)collision.gameObject.GetComponent ("Asteroid");
			asteroid.applyDamage(damage);
			//Attach explosion to asteroid
			exp.transform.parent = asteroid.transform;
		}
		
		//Eliminate bullet
		Destroy(gameObject);
	}
	
	void kill () {
		GameObject.Destroy(this.gameObject);
	}
}
