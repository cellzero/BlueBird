using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFly : MonoBehaviour {

    public int init_speed;
    public int amplitude;
    public float omega;

    public GameObject crow;
    public float wait_threshold;
    public float move_threshold;

    private int speed;
    private float initZ;
    private float initY;
    private float prevY;
    private float preAngle;

    //levitating animate
    private int levitate;
    private bool up;

	// Use this for initialization
	void Start () {
        initZ = transform.position.z;
        initY = prevY = transform.position.y;
        speed = init_speed;
        preAngle = -45;

        levitate = 0;
        up = false;
	}
	
	// Update is called once per frame
	void Update () {
        

        float dist = transform.transform.position.z - crow.transform.position.z;
        if (dist > wait_threshold)
        {
            speed = 0;
        }
        else if (dist < move_threshold && speed < init_speed) {
            speed++;
        }

        if (isStop())
        {
            if (levitate == 40) up = false;
            else if (levitate == -40) up = true;
            if (up)
            {
                transform.Translate(0, 0.05f, 0, Space.World);
                levitate++;
            }
            else
            {
                transform.Translate(0, -0.05f, 0, Space.World);
                levitate--;
            }
            
        }
        else {
            transform.Translate(0, amplitude * Mathf.Sin(omega * (transform.position.z - initZ)) + initY - prevY, Time.deltaTime * speed, Space.World);
            float angle = -Mathf.Atan(amplitude * omega * Mathf.Cos(omega * (transform.position.z - initZ))) * 180 / Mathf.PI;
            transform.Rotate(angle - preAngle, 0, 0, Space.Self);
            preAngle = angle;
            prevY = transform.position.y;
        }
    }
    public bool isStop() {
        if (speed == 0) return true;
        return false;
    }
}
