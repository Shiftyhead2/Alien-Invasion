using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public float SmoothSpeed;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 DesiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }


}
