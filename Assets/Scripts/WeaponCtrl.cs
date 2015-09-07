using UnityEngine;
using System.Collections;

public class WeaponCtrl : MonoBehaviour {
	
	public GameObject bulletPrefab;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			//Debug.Log("Pressed left click.");
			GameObject bullet = (GameObject)GameObject.Instantiate(bulletPrefab);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			bullet.transform.position += bullet.transform.forward * 12.0f;
		}
		if(Input.GetMouseButtonDown(1))
			Debug.Log("WeaponCtl - Pressed right click.");
		if(Input.GetMouseButtonDown(2))
			Debug.Log("WeaponCtl - Pressed middle click.");	
	}
}
