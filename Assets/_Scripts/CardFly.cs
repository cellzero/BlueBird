using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFly : MonoBehaviour {

    public int speed;
    public int amplitude;
    public float omega;

    private float initZ;
    private float initY;
    private float prevY;
    private float preAngle;

	// Use this for initialization
	void Start () {
        initZ = transform.position.z;
        initY = prevY = transform.position.y;
        preAngle = -45;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, amplitude*Mathf.Sin(omega*(transform.position.z-initZ)) + initY - prevY, Time.deltaTime * speed, Space.World);
        float angle = -Mathf.Atan(amplitude * omega * Mathf.Cos(omega * (transform.position.z - initZ))) * 180 / Mathf.PI;
        transform.Rotate(angle - preAngle, 0, 0, Space.Self);
        preAngle = angle;
        prevY = transform.position.y;
    }
}
