using System;
using UnityEngine;

[Serializable]
public class MouseLook:MonoBehaviour
{
    public Transform Turret;
    public Transform Camera;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MoveSensitivity = 1f;
    public float ZoomSensitivity = 5f;
    public bool clampVerticalRotation = true;
    public float MinAngle = -90F;
    public float MaxAngle = 90F;
    public bool smooth;
    public float smoothTime = 5f;

    public float MinDistance = 5;
    public float MaxDistance = 40;

    public float OffsetBorder = 10f;


    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    private bool r_pressed = false;

    void Start()
    {
        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = Turret.localRotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (r_pressed)
            {
                float yRot = Input.GetAxis("Mouse X") * XSensitivity * Time.deltaTime;
                float xRot = Input.GetAxis("Mouse Y") * YSensitivity * Time.deltaTime;

                m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
                m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

                if (clampVerticalRotation)
                    m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

                if (smooth)
                {
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, m_CharacterTargetRot,
                        smoothTime * Time.deltaTime);
                    Turret.localRotation = Quaternion.Slerp(Turret.localRotation, m_CameraTargetRot,
                        smoothTime * Time.deltaTime);
                }
                else
                {
                    transform.localRotation = m_CharacterTargetRot;
                    Turret.localRotation = m_CameraTargetRot;
                }
            }
            r_pressed = true;
        }
        else
        {
            r_pressed = false;
        }

        {
            float yMov = Input.GetAxis("Vertical") * MoveSensitivity * Time.deltaTime;
            float xMov = Input.GetAxis("Horizontal") * MoveSensitivity * Time.deltaTime;

            Vector3 moving = new Vector3(xMov, 0, yMov);
            if (moving != Vector3.zero)
            {
                moving = transform.position + transform.localToWorldMatrix.MultiplyVector(moving);
                float x = moving.x;
                float z = moving.z;
                if (x < -OffsetBorder) x = -OffsetBorder;
                if (x > Global.MapSize.X + OffsetBorder) x = Global.MapSize.X + OffsetBorder;
                if (z < -OffsetBorder) z = -OffsetBorder;
                if (z > Global.MapSize.Y + OffsetBorder) z = Global.MapSize.Y + OffsetBorder;
                transform.position = new Vector3(x, 8f, z);
            }
        }

        {
            float xMov = Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity * Time.deltaTime;

            Vector3 moving = Camera.localPosition + new Vector3(0, 0, xMov);
            if (moving.z > -MinDistance) moving = new Vector3(0, 0, -MinDistance);
            if (moving.z < -MaxDistance) moving = new Vector3(0, 0, -MaxDistance);

            Camera.localPosition = moving;
        }
    }


    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinAngle, MaxAngle);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
