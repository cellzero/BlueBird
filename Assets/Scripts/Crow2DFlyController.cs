using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow2DFlyController : MonoBehaviour {

    //public GameObject SpeedLine;

	public float MaxStrength;
	private float currentStrength;

	public float TimeToRecover;
	public Camera MainCamera;
	private float lastPushTime;

	public float OneStepRightMin;
	public float OneStepRightMax;
	private float currentOneStepRight;

	private Animator _animator;



	// Use this for initialization
	void Start () {

		lastPushTime = 0;

		_animator = this.GetComponent<Animator>();


		currentOneStepRight = OneStepRightMin;
		currentStrength = MaxStrength;


	}

	// Update is called once per frame
	void Update () {



		//transform.rotation=Quaternion.Euler(transform.rotation.x + 0.5f,transform.rotation.y, transform.rotation.z);    



		float cAX = transform.localEulerAngles.x;

		if (Input.GetAxis ("Vertical") > 0) {
			lastPushTime = Time.time;
            if (cAX != 0f && (cAX <= 325f && cAX >= 315f))
            {
            }
            else
            {
                transform.Rotate(-2.5f, 0f, 0f);
               // SpeedLine.transform.Rotate(-2.5f, 0f, 0f);
            }
		} else if (Input.GetAxis ("Vertical") < 0) {
			lastPushTime = Time.time;
            if (cAX != 0f && (cAX >= 35F && cAX <= 45f))
            {
            }
            else
            {
                transform.Rotate(2.5f, 0f, 0f);
               // SpeedLine.transform.Rotate(2.5f, 0f, 0f);
            }
		} else {
			float curTime = Time.time;
			if (curTime - lastPushTime > TimeToRecover) {
				if (cAX >= 300f) {
					transform.Rotate (Mathf.Min(0.9f, 360f - cAX), 0f, 0f);
                    //SpeedLine.transform.Rotate(Mathf.Min(0.9f, 360f - cAX), 0f, 0f);

                } else if (cAX > 0f) {
					transform.Rotate (Mathf.Max(-0.9f, -cAX), 0f, 0f);
                    //SpeedLine.transform.Rotate(Mathf.Max(-0.9f, -cAX), 0f, 0f);
                }
			}
		}

		if (Input.GetButton ("Jump") && currentStrength >= 0.5f) {
			currentStrength -= 0.5f;
			currentOneStepRight += 0.25f;
			if (currentOneStepRight >= OneStepRightMax)
				currentOneStepRight = OneStepRightMax;

		} else if (Input.GetButton ("Jump")) {

			// 无法继续加速 只能减

			currentOneStepRight -= 0.25f;
			if (currentOneStepRight < OneStepRightMin)
				currentOneStepRight = OneStepRightMin;
		} else if (currentStrength < MaxStrength){
			// 考虑减速和恢复能量

			currentStrength += 0.5f;


			currentOneStepRight -= 0.5f;
			if (currentOneStepRight < OneStepRightMin)
				currentOneStepRight = OneStepRightMin;
		}

		print (currentOneStepRight + " " + currentStrength);



		cAX = transform.localEulerAngles.x;


		if (cAX >=270) _animator.SetBool("isUpFlying", true);
		else _animator.SetBool("isUpFlying", false);


		float rightSpeed = Mathf.Cos (Mathf.Deg2Rad * cAX);
		float downSpeed = Mathf.Sin (Mathf.Deg2Rad * cAX);



		Vector3 v = transform.position;
		v.z = v.z + currentOneStepRight * Time.deltaTime;

		v.y = v.y - downSpeed / rightSpeed * currentOneStepRight * Time.deltaTime;
		if (v.y <= 5f)
			v.y = 5f;
		if (v.y >= 75f)
			v.y = 75f;

		transform.position = v;

		// MainCamera.transform.position = MainCamera.transform.position + new Vector3 (0, 0, OneStepRight);


		// MainCamera.transform.position = MainCamera.transform.position + new Vector3 (0, offset_y + transform.position.y - MainCamera.transform.position.y, 0);

	}
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Element")
        Destroy(other.gameObject);
    }
}
