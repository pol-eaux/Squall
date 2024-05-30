using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool canJump;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundingRaycastDistance;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    private void Jump()
    {
        // If the jump feature is enabled.
        if (canJump)
        {
            // When jump key is pressed.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // If the player is on the ground, jump.
                if(IsGrounded())
                {
                    rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                }
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundingRaycastDistance);
    }
}
