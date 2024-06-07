using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_old : MonoBehaviour
{
    [Header("Feature Toggles")]
    [SerializeField] private bool canJump;
    [SerializeField] private bool canCrouch;
    [SerializeField] private bool canSprint;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchWalkSpeed;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundingRaycastDistance;

    [Header("Crouch Variables")]
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float crouchHeight = 1.4f;
    [SerializeField] private float timeToCrouch = 0.1f;
    [SerializeField] private float crouchingRaycastDistance;
    private bool isCrouching;
    private bool duringCrouchAnimation;

    // Private non serialized variables.
    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    Vector3 standingCenter = new Vector3(0, 0, 0);
    private Rigidbody rb;
    private CapsuleCollider playerBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerBody = GetComponent<CapsuleCollider>();
        isCrouching = false;
    }

    private void Update()
    {
        Jump();
        Crouch();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        float speed;

        // Calculate movement speed.
        if(isCrouching)
        {
            speed = crouchWalkSpeed;
        }
        else if(canSprint && Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

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

    private void Crouch()
    {
        if(canCrouch)
        {
            if (Input.GetKeyDown(KeyCode.C) && !duringCrouchAnimation && IsGrounded())
            {
                StartCoroutine(CrouchStand());
            }
        }
    }

    private IEnumerator CrouchStand()
    {
        // If there is an object above, don't stand up.
        if(isCrouching && Physics.Raycast(transform.position, Vector3.up, crouchingRaycastDistance))
        {
            yield break;
        }
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = playerBody.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = playerBody.center;

        while (timeElapsed < timeToCrouch)
        {
            playerBody.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            playerBody.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerBody.height = targetHeight;
        playerBody.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundingRaycastDistance);
    }
}
