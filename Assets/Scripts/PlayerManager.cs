using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Movement speed of the character
    private float moveSpeed = 5f;

    // Rigidbody component attached to the character
    private Rigidbody2D rb;

    // Box collider component attached to the character
    private BoxCollider2D collider;

    void Start() {
        // Get the Rigidbody and Box Collider components
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update() {

        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the character based on the input
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);

    }
}
