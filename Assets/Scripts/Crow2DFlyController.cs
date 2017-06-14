using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow2DFlyController : MonoBehaviour
{

    public float TimeToRecover;
    private float lastPushTime;
    public float OneStepRight;

    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        lastPushTime = 0;

        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation=Quaternion.Euler(transform.rotation.x + 0.5f,transform.rotation.y, transform.rotation.z);    

        float cAX = transform.localEulerAngles.x;

        if (Input.GetAxis("Vertical") > 0)
        {
            lastPushTime = Time.time;
            if (cAX != 0f && (cAX <= 325f && cAX >= 315f))
            {
            }
            else

                transform.Rotate(-1.5f, 0f, 0f);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            lastPushTime = Time.time;
            if (cAX != 0f && (cAX >= 35F && cAX <= 45f))
            {
            }
            else
                transform.Rotate(1.5f, 0f, 0f);
        }
        else
        {
            float curTime = Time.time;
            if (curTime - lastPushTime > TimeToRecover)
            {
                if (cAX >= 300f)
                {
                    transform.Rotate(Mathf.Min(0.9f, 360f - cAX), 0f, 0f);
                }
                else if (cAX > 0f)
                {
                    transform.Rotate(Mathf.Max(-0.9f, -cAX), 0f, 0f);
                }
            }
        }

        cAX = transform.localEulerAngles.x;

        if (cAX >= 270) _animator.SetBool("isUpFlying", true);
        else _animator.SetBool("isUpFlying", false);

        float rightSpeed = Mathf.Cos(Mathf.Deg2Rad * cAX);
        float downSpeed = Mathf.Sin(Mathf.Deg2Rad * cAX);

        Vector3 v = transform.position;
        v.z = v.z + OneStepRight;

        v.y = v.y - downSpeed / rightSpeed * OneStepRight;
        if (v.y <= 5f)
            v.y = 5f;
        if (v.y >= 75f)
            v.y = 75f;

        transform.position = v;
    }
}
