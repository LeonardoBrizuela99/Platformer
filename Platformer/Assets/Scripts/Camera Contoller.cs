using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 rotationSpeed; 
    [SerializeField] private float distanceFromTarget = 5.0f; 
    [SerializeField] private Vector3 offset; 
    [SerializeField] private float smoothTime=0.3f; 

    private Vector3 currentRotation;
    private Vector3 currentVelocity;

    void Start()
    {
        currentRotation = transform.eulerAngles;
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 direction = target.forward;

        Vector3 desiredPosition = target.position - direction * distanceFromTarget + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

        transform.LookAt(target.position + offset);

    }

    public void RotateCamera(Vector2 delta)
    {
        var scaledDelta = Vector2.Scale(delta, rotationSpeed) * Time.deltaTime;

        currentRotation.x += scaledDelta.y;
        currentRotation.y += scaledDelta.x;

        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distanceFromTarget + offset;

        transform.rotation = rotation;
        transform.position = position;
    }
}
