using UnityEngine;
using System.Collections;

public class WrapperInitializer : MonoBehaviour {
	
	public GameObject wrapTop;
	public GameObject wrapBottom;
	public GameObject wrapLeft;
	public GameObject wrapRight;
	
	public float wrapperDelta = 20.0f;
	
	private Plane ground;
	
	// Use this for initialization
	void Start () {
		ground = new Plane(Vector3.up, Vector3.zero);
		
		//Find min x-z, assume rectangle.
		Ray ray = Camera.main.ScreenPointToRay(Vector2.zero);
		float enter;
		bool rch = ground.Raycast(ray, out enter);
		Vector3 position = ray.GetPoint(enter);
		
		// Should not happen...
		if(!rch){ Debug.LogError("Could not cast ray to ground plane"); }
		
		//Initialize wrapper positions assuming rectangular camera view
		wrapTop.transform.position = new Vector3(0, 0, -position.z + wrapperDelta);
		wrapBottom.transform.position = new Vector3(0, 0, position.z - wrapperDelta);
		wrapRight.transform.position = new Vector3(-position.x + wrapperDelta, 0, 0);
		wrapLeft.transform.position = new Vector3(position.x - wrapperDelta, 0, 0);
		
		((WrapAround) wrapTop.GetComponent("WrapAround")).initialize();
		((WrapAround) wrapBottom.GetComponent("WrapAround")).initialize();
		((WrapAround) wrapRight.GetComponent("WrapAround")).initialize();
		((WrapAround) wrapLeft.GetComponent("WrapAround")).initialize();
	}
}
