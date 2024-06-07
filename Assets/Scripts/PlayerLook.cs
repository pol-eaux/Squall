using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float xRotation = 0f;
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;

    private Transform playerBody;
    private Camera cam;

    public void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // Calculate cam rotation for looking up and down.
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        // Apply to camera transform.
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // Rotate player to look left and right.
        playerBody.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponentInParent<Transform>();
        cam = GetComponentInChildren<Camera>();
    }
}
