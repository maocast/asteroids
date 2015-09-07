using UnityEngine;
using System.Collections;

public class WrapAround : MonoBehaviour {
	
	public GameObject oppositeGO;
	public float deltaPosGen = 20.0f; //Offset where to "teleport" the objects
	
	private WrapAround opposite;
	private Vector3 translation;
	private Vector3 unitTranslation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void initialize () {
		opposite = (WrapAround)oppositeGO.GetComponent("WrapAround");
		translation = oppositeGO.transform.position - transform.position;
		unitTranslation = translation.normalized;
	}
	
	void OnTriggerEnter(Collider obj)
	{
		opposite.wrapObject(obj.gameObject);
	}
	
	void wrapObject(GameObject obj)
	{
		//Keep linear and angular velocities for "reassignment"
		Vector3 velocity = obj.GetComponent<Rigidbody> ().velocity;
		Vector3 angularVelocity = obj.GetComponent<Rigidbody> ().angularVelocity;

		//Disable physics - Allows for transform.position direct manipulation
		obj.GetComponent<Rigidbody>().isKinematic = true;
		
		//Last term offsets the placement of the item
		obj.transform.position = obj.transform.position - translation + (deltaPosGen * unitTranslation);
		
		//Enable physics
		obj.GetComponent<Rigidbody>().isKinematic = false;

		//Reassign velocities
		obj.GetComponent<Rigidbody> ().velocity = velocity;
		obj.GetComponent<Rigidbody> ().angularVelocity = angularVelocity;
	}
}
