using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float distance = 5f;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float scrollSensitivity = 1;
    public bool RotateAroundPlayer = true;
    //public float RotationsSpeed = 5.0f;
    public Vector3 _cameraOffset;

    void Start()
    {
    _cameraOffset = transform.position - target.position;
    }

    void LateUpdate()
    {

        //Following
        if (!target)
        {
            print("No Target Set for the camera");
            return;
        }

        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance, 1f, 20f);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);

    }

    
}
