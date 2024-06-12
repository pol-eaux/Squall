using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Feature Toggles")]
    [SerializeField] private bool useJump = true;
    [SerializeField] private bool useCrouch = true;
    [SerializeField] private bool useSprint = true;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float crouchWalkSpeed = 2.5f;
    private bool isSprinting;
    private float currentSpeed;

    [Header("Jump Variables")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    private bool isGrounded;

    [Header("Crouch Variables")]
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float crouchHeight = 1.4f;
    [SerializeField] private float timeToCrouch = 0.1f;
    [SerializeField] private float crouchingRaycastDistance;
    [SerializeField] private bool isCrouching;
    private bool duringCrouchAnimation;
    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    Vector3 standingCenter = new Vector3(0, 0, 0);

    // Private references.
    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        isCrouching = false;
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    // Receives inputs from InputManager and applies them to the character controller.
    public void Move(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // If crouching set speed to crouch walk speed.
        if(isCrouching)
        {
            currentSpeed = crouchWalkSpeed;
        }

        // If not crouching or sprinting, set speed to walk speed.
        if(!isCrouching && !isSprinting)
        {
            currentSpeed = walkSpeed;
        }

        characterController.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        // If the jump feature is enabled, and the player is grounded...
        if (isGrounded && useJump)
        {
            // And the player is grounded, jump.
            if(!isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
    }

    public void Crouch()
    {
        if (useCrouch)
        {
            if (!duringCrouchAnimation && characterController.isGrounded)
            {
                StartCoroutine(CrouchStand());
            }
        }
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;
        // If the player is not crouching, and is grounded...
        if(!isCrouching && isGrounded)
        {
            // And if the sprint feature is enabled, and the player is sprinting, set speed to sprint speed.
            if (isSprinting && useSprint)
            {
                currentSpeed = sprintSpeed;
            }
            // If the sprint input is released, no longer sprinting, set the speed to walk speed.
            else
            {
                currentSpeed = walkSpeed;
            }
        }
    }

    private IEnumerator CrouchStand()
    {
        // If there is an object above, don't stand up.
        if (isCrouching && Physics.Raycast(transform.position, Vector3.up, crouchingRaycastDistance))
        {
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isCrouching = !isCrouching;

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        duringCrouchAnimation = false;
    }

}
