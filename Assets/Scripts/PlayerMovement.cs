using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle movement
        Move();

        // Handle rotation towards the mouse
        RotateTowardsMouse();
    }

    // Handles player movement
    private void Move()
    {
        float speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        float speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }

    // Rotates the player to face the mouse cursor
    private void RotateTowardsMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the player to face the mouse (subtract 90 to correct rotation)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}

