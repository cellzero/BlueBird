using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueBird.Demos
{
    public class FixedDirCamera : MonoBehaviour
    {
        [SerializeField] public Transform player;
        private Vector3 offset;

        void Start()
        {
            offset = player.position - transform.position;
        }

        void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * 5);
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
        }
    }
}
