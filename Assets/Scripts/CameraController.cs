using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    
    public Transform target;
    public float targetInterp;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    public float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start() {
        target = FindObjectOfType<PlayerManager>().transform;
    }

    void FixedUpdate()
    {
        var secondTargetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 cameraPosition = transform.position;
        Vector3 targetPosition = Vector3.Lerp(target.position, secondTargetPosition, targetInterp);

        // Calculate the new position of the camera based on the target's position
        cameraPosition = Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, smoothTime);

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x, maxBounds.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y, maxBounds.y);
        cameraPosition.z = -10; // offset above the target

        // Update the camera's position with the clamped values
        transform.position = cameraPosition;
    }
}
