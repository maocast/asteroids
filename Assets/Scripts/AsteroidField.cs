using UnityEngine;
using System.Collections;

/**
 * Asteroid Field class
 * Contains the prefab asteroids and a counter for the number of *top* (unsplit)
 * asteroids currently instantiated.
 */
public class AsteroidField : MonoBehaviour {
	
	public GameObject[] asteroids;
	public int maxAsteroids = 10;
	public GUIText scoreGUI;
	
	private int numAsteroids;
	private int score;
	
	// Use this for initialization
	void Start () {
		numAsteroids = 0;
		score = 0;
		
		updateGUI();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void generateAsteroid(AsteroidGenerator generator)
	{
		//Randomly select asteroid
		int asteroidPos = Mathf.RoundToInt(Random.value * (asteroids.Length - 1));
		GameObject asteroidPrefab = asteroids[asteroidPos];
		
		//Instantiate asteroid and place in the generator's location
		GameObject asteroid = (GameObject) GameObject.Instantiate(asteroidPrefab, generator.transform.position, generator.transform.rotation);
		asteroid.transform.parent = transform;
		
		//Set reference to asteroid field
		((Asteroid)asteroid.GetComponent ("Asteroid")).setAsteroidField(this);
		
		//Add to asteroid count
		numAsteroids++;
	}
	
	public void destroyAsteroid (Asteroid asteroid) {
				//Update player score
		if (asteroid.smallerRock != null) {
			GameObject rock1 = (GameObject) GameObject.Instantiate(asteroid.smallerRock, asteroid.transform.position, asteroid.transform.rotation);
			GameObject rock2 = (GameObject) GameObject.Instantiate(asteroid.smallerRock, asteroid.transform.position, Quaternion.Inverse(asteroid.transform.rotation));
			
			rock1.GetComponent<Rigidbody>().velocity = asteroid.GetComponent<Rigidbody>().velocity;
			rock2.GetComponent<Rigidbody>().velocity = asteroid.GetComponent<Rigidbody>().velocity;
			
			rock1.transform.parent = transform;
			rock2.transform.parent = transform;
			
			((Asteroid)rock1.GetComponent("Asteroid")).setAsteroidField(this);
			((Asteroid)rock2.GetComponent("Asteroid")).setAsteroidField(this);
			
			numAsteroids += 2;
		}
		
		GameObject explosion = (GameObject) GameObject.Instantiate(asteroid.asteroidExplosion, asteroid.transform.position, asteroid.transform.rotation);
		explosion.transform.parent = asteroid.transform;
		asteroid.GetComponent<Renderer>().enabled = false;
		asteroid.GetComponent<Collider>().enabled = false;
		asteroid.Invoke("kill", 2.0f);
		
		score += asteroid.scorePoints;
		numAsteroids--;
		
		updateGUI();
	}
	
	private void updateGUI()
	{
		scoreGUI.text = "Score: " + score;
	}
}
