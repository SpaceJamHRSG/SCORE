using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // The target to follow
    public Transform target;

    // The bounds of the rectangle to clamp the camera within
    public Vector2 minBounds;
    public Vector2 maxBounds;

    public float smoothTime = 0.25f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate() {
        target = FindObjectOfType<PlayerManager>().transform;
        Vector3 cameraPosition = transform.position;

        // Calculate the new position of the camera based on the target's position
        cameraPosition = Vector3.SmoothDamp(cameraPosition, target.position, ref velocity, smoothTime);

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x, maxBounds.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y, maxBounds.y);
        cameraPosition.z = -10; // offset above the target

        // Update the camera's position with the clamped values
        transform.position = cameraPosition;
    }
}
