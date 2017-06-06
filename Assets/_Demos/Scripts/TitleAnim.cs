using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueBird.Demos
{

    [System.Serializable]
    public class AnimDescriber
    {
        public string animName;
        public float animRate;
    }

    public class TitleAnim : MonoBehaviour
    {

        public AnimDescriber[] animFlag;
        public Animator anim;

        public float idleWait;
        private float idleStartTime;

        private int preAnimId;

        // Use this for initialization
        void Start()
        {
            idleStartTime = 0.0f;
            preAnimId = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (animFlag.Length <= 0)
                return;

            if (Time.time - idleStartTime > idleWait)
            {
                idleStartTime = Time.time;

                float[] p = new float[animFlag.Length + 1];
                p[0] = 0.0f;

                for (int i = 0; i < animFlag.Length; i++)
                    p[i + 1] = p[i] + animFlag[i].animRate;

                float r = Random.Range(p[0], p[p.Length - 1]);
                int index = 0;
                for (int i = 0; i < animFlag.Length; i++)
                {
                    if (r <= p[i + 1])
                    {
                        index = i;
                        break;
                    }
                }

                anim.SetBool(animFlag[preAnimId].animName, false);
                anim.SetBool(animFlag[index].animName, true);
                preAnimId = index;
            }
        }
    }
}
