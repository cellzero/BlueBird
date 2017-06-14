using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {
	public GameObject crow;

	private float offset_z, offset_y;


	// Use this for initialization
	void Start () {
		offset_y = transform.position.y - crow.transform.position.y;
		offset_z = transform.position.z - crow.transform.position.z;
	}
	
	// Update is called once per frame
	void LateUpdate () {


		transform.position = transform.position + new Vector3 (0, offset_y + crow.transform.position.y - transform.position.y, crow.transform.position.z - transform.position.z + offset_z);


		// transform.position = transform.position + new Vector3 (0, offset_y + crow.transform.position.y - transform.position.y, 0);



	}
}
