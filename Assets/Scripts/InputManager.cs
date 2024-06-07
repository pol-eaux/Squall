using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponentInChildren<PlayerCamera>();

        onFoot.Jump.performed += ctx => playerMovement.Jump();
        onFoot.Crouch.performed += ctx => playerMovement.Crouch();
        onFoot.Sprint.performed += ctx => playerMovement.Sprint();
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    void Update()
    {
        // Tell player movement to move using the value from movement input.
        playerMovement.Move(onFoot.Movement.ReadValue<Vector2>());
        playerCamera.RotateCamera(onFoot.Look.ReadValue<Vector2>());
    }
}
