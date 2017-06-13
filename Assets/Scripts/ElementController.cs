using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour {

    public float aim_scale;
    private float scale_x;

	// Use this for initialization
	void Start () {
        scale_x = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15,30,35) * Time.deltaTime);
        if (scale_x < aim_scale)
        {
            scale_x += 0.01f;
            transform.localScale = new Vector3(scale_x, scale_x, scale_x);
        }
	}
}
