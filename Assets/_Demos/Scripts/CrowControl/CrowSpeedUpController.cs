using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueBird.Demos
{
    public class CrowSpeedUpController : MonoBehaviour
    {
        #region Attributes
        [SerializeField]
        private float m_MaxSpeed = 5.0f;
        [SerializeField]
        private float m_MinSpeed = 1.0f;
        private float m_CurrentSpeed = 0.0f;

        [SerializeField]
        private float m_RotationSpeed = 100.0f;

        [SerializeField]
        private float m_YawSensibility = 1.0f;

        [SerializeField]
        private float m_ResetRotateTime = 1.0f;
        private float m_ResetRotateTimer = 0;

        [SerializeField]
        [Range(0, 90)]
        private float m_PitchLimit = 45.0f;
        [SerializeField]
        [Range(0, 90)]
        private float m_RollLimit = 45.0f;
        #endregion

        #region MonoBehaviour
        void Start() { }

        void Update()
        {
            MoveRotate();
            MovePosition();
        }
        #endregion

        #region Private Manipulators
        /// <summary>
        /// 执行所有旋转操作。
        /// </summary>
        private void MoveRotate()
        {
            float pitchAxis = Input.GetAxis("Vertical");
            float rollAxis = Input.GetAxis("Horizontal");

            // 执行“上下”方向的旋转（pitch）
            if (pitchAxis != 0)
            {
                // “向上”按键，物体将向下看
                // “向下”按键，物体将向上看
                _RotatePitch(pitchAxis);
            }

            // 执行“左右倾斜”的旋转（roll）
            if (rollAxis != 0)
            {
                // “向右”按键，物体将向右旋转
                // “向左”按键，物体将向左旋转
                // 因为旋转轴（此处为transform.forward）的逆时针为正方向，而输入的向右为正防线，故将输入取负
                _RotateRoll(-rollAxis);
            }

            // 根据“左右倾斜”的角度，执行“左右”方向的旋转
            _RotateYawFromRoll();

            // 一段时间没有方向输入，将旋转重置
            _RotateReset(pitchAxis == 0 && rollAxis == 0);
        }

        /// <summary>
        /// 执行移动操作。
        /// 加速能够使速度变为2倍。
        /// 向上飞行，速度会下降；向下飞行，速度会提升。
        /// </summary>
        private void MovePosition()
        {
            // 如果按下“加速”键，速度将变为两倍
            if (Input.GetAxis("SpeedUp") > 0)
            {
                m_CurrentSpeed = Mathf.Lerp(m_CurrentSpeed, m_MaxSpeed, Time.deltaTime);
            }
            else
            {
                m_CurrentSpeed = Mathf.Lerp(m_CurrentSpeed, m_MinSpeed, Time.deltaTime);
            }

            // 沿物体坐标的z方向（向里），移动物体
            transform.Translate(0, 0, Time.deltaTime * m_CurrentSpeed);
        }

        /// <summary>
        /// 执行“上下”方向的旋转。
        /// </summary>
        /// <param name="_AdditivePitch">垂直方向的增量程度。</param>
        private void _RotatePitch(float _AdditivePitch)
        {
            // 如果没有超过限度
            if (CheckPitchLimit(_AdditivePitch))
            {
                // 则以世界坐标系中，物体的右方向为轴，进行旋转
                _AdditivePitch *= Time.deltaTime * m_RotationSpeed;
                Quaternion rotator = Quaternion.AngleAxis(_AdditivePitch, transform.right);
                transform.rotation = rotator * transform.rotation;
            }
        }

        /// <summary>
        /// 执行“左右倾斜”的旋转。
        /// </summary>
        /// <param name="_AdditiveRoll">水平方向的增量程度。</param>
        private void _RotateRoll(float _AdditiveRoll)
        {
            // 如果没有超过限度
            if (CheckRollLimit(_AdditiveRoll))
            {
                // 则以世界坐标系中，物体的前方向为轴，进行旋转
                _AdditiveRoll *= Time.deltaTime * m_RotationSpeed;
                Quaternion rotator = Quaternion.AngleAxis(_AdditiveRoll, transform.forward);
                transform.rotation = rotator * transform.rotation;
            }
        }

        /// <summary>
        /// 根据“左右倾斜”的角度，执行“左右”方向的旋转。
        /// </summary>
        private void _RotateYawFromRoll()
        {
            float yawSensibility = m_YawSensibility;

            // 世界坐标系中，物体右方向且平行于地面的单位向量
            // 强制令“y = 0”，不是一种精确的表达，但用来表达趋势，这种近似已经足够
            Vector3 rightNoY = transform.right;
            rightNoY.y = 0;
            rightNoY.Normalize();

            // 用cos值模拟旋转角度
            float dot = Vector3.Dot(transform.up, rightNoY);

            // 如果物体的上方向，垂直于地面（即cos值为0），则不用旋转
            if (dot != 0)
            {
                yawSensibility *= dot;
                Quaternion rotator = Quaternion.AngleAxis(yawSensibility, Vector3.up);
                transform.rotation = rotator * transform.rotation;
            }
        }

        /// <summary>
        /// 一段时间没有方向输入，将旋转重置。
        /// </summary>
        /// <param name="_IsMovePressed">是否有移动相关的输入。</param>
        private void _RotateReset(bool _IsMovePressed)
        {
            if (_IsMovePressed)
            {
                // 计时器时间到
                if (m_ResetRotateTimer > m_ResetRotateTime)
                {
                    // 计算世界坐标系下，平行于地面的单位前向量
                    Vector3 forwardNoY = transform.forward;
                    forwardNoY.y = 0;
                    forwardNoY.Normalize();

                    // 当前角度，到“平行于地面的前方向，垂直于地面的上方向”的角度的插值
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forwardNoY), Time.deltaTime);
                }

                // 更新计时器
                m_ResetRotateTimer += Time.deltaTime;
            }
            else
            {
                // 有方向键按下，清零计时器
                m_ResetRotateTimer = 0;
            }
        }

        /// <summary>
        /// 判断是否达到“上下旋转”上限。
        /// </summary>
        /// <param name="_AdditivePitch">垂直方向的增量程度。</param>
        /// <returns>是否达到“上下旋转”上限。</returns>
        private bool CheckPitchLimit(float _AdditivePitch)
        {
            // 如果旋转没有限制，直接返回true
            if (m_PitchLimit <= 0)
            {
                return true;
            }

            // 移动方向与旋转方向相反
            // 必然使旋转角度减小，直接返回true
            // 判断方式：forward.y标志了物体仰角，为正表示上仰；_AdditivePitch标志旋转方向，为负上仰。
            //  故取两者相乘 > 0。
            if (transform.forward.y * _AdditivePitch > 0)
            {
                return true;
            }

            // 物体真实的前向量
            // 注：不能用forward.y强制为0的单位向量
            //  因为这个向量只是“forward”投影到“y=0平面”的向量
            //  其计算角度会小于真实倾斜角度
            Vector3 forwardNoY = Vector3.Cross(transform.right, Vector3.up);
            forwardNoY.Normalize();

            // 计算当前的旋转角度
            float pitch = Vector3.Angle(transform.forward, forwardNoY);
            if (pitch + _AdditivePitch > m_RollLimit)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断是否达到“水平倾斜”上限。
        /// </summary>
        /// <param name="_AdditiveRoll">水平方向的增量程度。</param>
        /// <returns>是否达到“水平倾斜”上限。</returns>
        private bool CheckRollLimit(float _AdditiveRoll)
        {
            // 如果左右旋转没有限制，直接返回true
            if (m_RollLimit <= 0)
            {
                return true;
            }

            // 移动方向与旋转方向相反
            // 必然使旋转角度减小，直接返回true
            if (transform.right.y * _AdditiveRoll < 0)
            {
                return true;
            }

            // 物体真实的右向量
            Vector3 rightNoY = Vector3.Cross(Vector3.up, transform.forward);
            rightNoY.Normalize();

            // 计算当前的旋转角度
            float roll = Vector3.Angle(transform.right, rightNoY);
            if (roll + _AdditiveRoll > m_RollLimit)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
