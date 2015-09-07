using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour {
	//Reference to the asteroid container
	public GameObject asteroidField;
	
	//Rates for asteroid generation
	public float genRate = 2.0f;
	public float genProb = 0.5f;
	
	private AsteroidField afScript;
	
	// Use this for initialization
	void Start () {
		afScript = (AsteroidField)((GameObject)GameObject.Find("AsteroidField")).GetComponent("AsteroidField");
		InvokeRepeating("generateAsteroid", 1.0f, genRate);
	}
	
	void generateAsteroid () {
		float rand = Random.value;
		if(rand < genProb)
		{
			//Generate new asteroid
			afScript.generateAsteroid(this);
		}
	}
}
