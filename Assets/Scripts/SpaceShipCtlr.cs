using UnityEngine;
using System.Collections;

public class SpaceShipCtlr : MonoBehaviour {
	//shuttle's rotatation speed
	public float rotationSpeed = 10.0f;
	//Shuttle's translation speed
	public float speed = 10.0f;
	//Reference to the particle emitter for the movement effect
	public GameObject particleEmitterGO;
	
	//Internal reference to particle emitter
	private ParticleSystem pEmitter;
	//Internal reference to the "ground" plane for rotation
	private Plane ground;

	// Use this for initialization
	void Start () {
		ground = new Plane(Vector3.up, Vector3.zero); //Intersects 0,0 and points upward
		pEmitter = (ParticleSystem)particleEmitterGO.transform.GetChild(0).GetComponent("ParticleSystem");
	}
 
 	void Update() {
		rotateShuttle();
		generateParticleEmissions();
	}
	
	void rotateShuttle() {
		//Find where mouse intersects with shuttle's plane
		float distance; //Distance from camera to 0,0,0 plane
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool rch = ground.Raycast(ray, out distance);
		Vector3 position = ray.GetPoint(distance);
		
		if(!rch){Debug.LogError("No ray collision to calculate rotation");} // Should not happen...
		
		float yRotation = Mathf.Rad2Deg * Mathf.Atan2(position.x, position.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0,yRotation,0)), Time.deltaTime * rotationSpeed);
	}
	
	void generateParticleEmissions() {
		//Rotate particle emitter based on keys that are pressed
		float pYRotation = Mathf.Rad2Deg * Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		//Relative to shuttle dirrection
		//particleEmitterGO.transform.rotation = Quaternion.Euler(new Vector3(0,pYRotation,0)) * transform.rotation;
		particleEmitterGO.transform.rotation = Quaternion.Euler(new Vector3(0,pYRotation,0));
		
		//Enable particle emitter if there is user input
		pEmitter.enableEmission = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

	}
	
	void OnTriggerEnter(Collider collider)
	{
		Time.timeScale = 0.5f;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
		Vector3 campos = Camera.main.transform.position;
		campos.y = 60f;
		Camera.main.transform.position = campos;
		
		Invoke ("normal", 1.0f);
		
		Debug.Log ("SpaceShipCtrl - IM HIT!");
	}
	
	void normal()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		Vector3 campos = Camera.main.transform.position;
		campos.y = 100f;
		Camera.main.transform.position = campos;
	}
}