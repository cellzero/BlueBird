using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueBird.Demos
{
    public class SimpleFolllowerCamera : MonoBehaviour
    {

        [SerializeField] private float distance = 10.0f;
        [SerializeField] private float height = 1.0f;
        [SerializeField] private Transform target = null;
        [SerializeField] private bool isCameraRoll = false;

        // Update is called once per frame
        void LateUpdate()
        {
            if (target != null)
            {
                Vector3 targetPosition = target.position - target.forward * distance + target.up * height;
                Quaternion targetRotation;

                if (isCameraRoll)
                    targetRotation = Quaternion.LookRotation(target.forward, target.up);
                else
                    targetRotation = Quaternion.LookRotation(target.forward, Vector3.up);

                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
            }
        }
    }
}
