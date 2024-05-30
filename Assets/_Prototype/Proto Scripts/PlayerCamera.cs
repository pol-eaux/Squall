using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos += smoothedVelocity;

        // Check for inversion.
        if (invertY)
        {
            transform.localRotation = Quaternion.AngleAxis(currentLookingPos.y, Vector3.right);
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        }

        // Check for inversion.
        if(invertX)
        {
            player.transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.x, player.transform.up);
        }
        else
        {
            player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
        }

        currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -90f, 90f);
    }
}
