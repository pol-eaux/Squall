using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadbobController : MonoBehaviour
{
    [Header("Feature Toggles")]
    [SerializeField] private bool useHeadbob;

    [Header("Headbob Variables")]
    [SerializeField] private float walkingBobbingSpeed = 14f;
    [SerializeField] private float crouchingBobbingSpeed = 7f;
    [SerializeField] private float bobbingAmount = 0.05f;

    //Private unserialized variables
    private float defaultPosY = 0;
    private float timer = 0;

    //References
    private PlayerMovement playerMovement;

    /// <summary>
    /// Set reference to player movement.
    /// </summary>
    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    /// <summary>
    /// Set the default camera position.
    /// </summary>
    private void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    /// <summary>
    /// Depending on player movement, adjust the camera position.
    /// If the player is idle, reset the position back to normal.
    /// </summary>
    private void Update()
    {
        // If headbob is disabled, return without doing anything.
        if (!useHeadbob) return;
        if (Mathf.Abs(playerMovement.GetMoveDirection().x) > 0.1f || Mathf.Abs(playerMovement.GetMoveDirection().z) > 0.1f)
        {
            //Player is moving
            timer += Time.deltaTime * (playerMovement.GetIsCrouching() ? crouchingBobbingSpeed : walkingBobbingSpeed);
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
