using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {
	public GameObject crow;

	private float offset_y;


	// Use this for initialization
	void Start () {
		offset_y =  transform.position.y - crow.transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {


		transform.position = transform.position + new Vector3 (0, 0, crow.transform.position.z - transform.position.z);


		transform.position = transform.position + new Vector3 (0, offset_y + crow.transform.position.y - transform.position.y, 0);



	}
}
