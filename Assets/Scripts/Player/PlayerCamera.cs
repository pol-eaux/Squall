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

    public void RotateCamera(Vector2 input)
    {
        input = Vector2.Scale(input, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, input.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, input.y, 1f / smoothing);

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
