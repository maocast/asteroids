using UnityEngine;
using System.Collections;

public class ShuttleMovement : MonoBehaviour {
	
	private SpaceShipCtlr shuttle;
	
	void Start () {
		shuttle = (SpaceShipCtlr)(GameObject.Find("Shuttle").GetComponent("SpaceShipCtlr"));
	}
	
	// Moves object using shuttle as reference
	void Update () {
		//Vector3 scaled =  -(shuttle.rotation * new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis ("Vertical")));
		Vector3 scaled = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		Vector3 newVelocity = -(scaled*shuttle.speed*Time.deltaTime);
		
		//TODO: Check that we don't go over max speed
		GetComponent<Rigidbody>().velocity += newVelocity;
	}
}
