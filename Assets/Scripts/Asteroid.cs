using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	//Initial properties for asteroid
	public float speed = 10.0f;
	public float hitPoints = 2.0f;
	public int scorePoints = 2;
	
	public GameObject asteroidExplosion;
	public GameObject smallerRock;
	
	//Reference to asteroid field
	private AsteroidField afScript;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = speed * transform.forward;
	}
	
	public void applyDamage(float damage)
	{
		hitPoints -= damage;
		if(hitPoints <= 0)
		{
			afScript.destroyAsteroid(this);
		}
	}
	
	public void setAsteroidField(AsteroidField af){
		afScript = af;
	}
	
	public void kill(){
		Destroy(gameObject);
	}
}
