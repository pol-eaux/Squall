using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    // The max distance you can interact with an object.
    [SerializeField] float interactionDistance;

    // The target you are currently looking at.
    private IInteractable currentTarget;

    // If this is true then interaction won't happen.
    private bool interactionDisabled;

    private void Awake()
    {
        interactionDisabled = false;
    }

    /// <summary>
    /// Call HandleRaycast, if the interact key if pressed,
    /// if the player is looking at a valid object, call it's
    /// OnInteract function.
    /// </summary>
    private void Update()
    {
        HandleRaycast();
        //Debug.Log("Raycast Disabled: " + _disabled);
        if (!interactionDisabled)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentTarget != null)
                {
                    currentTarget.OnInteract();
                }

            }
        }
    }

    /// <summary>
    /// Shoot a raycast, if it hit something interactable:
    ///     1. If the interactable has already been set as the current target: do nothing.
    ///     2. If the current target is not null and is not the current interactable:
    ///        call the end look function of the old target, and set this interactable as the new one,
    ///        then call the start look function of the new interactable.
    ///     3. Otherwise, set the interactable as the current target, call the start look function.
    /// If it did not hit an interactable:
    ///     1. If the current target is set to something else:
    ///        call the end look function, and set the current target to null.
    /// If nothing was hit at all by the raycast:
    ///     1. If the current target is set to something else:
    ///        Call the end look function, and set the current target to null.
    /// </summary>
    private void HandleRaycast()
    {
        RaycastHit whatIHit;
        // If the raycast hit something
        if (Physics.Raycast(transform.position, transform.forward, out whatIHit, interactionDistance))
        {
            IInteractable interactable = whatIHit.collider.GetComponent<IInteractable>();
            // If the object hit was interactable
            if (interactable != null)
            {
                if (interactable == currentTarget)
                {
                    return;
                }
                else if (currentTarget != null)
                {
                    currentTarget.OnEndLook();
                    currentTarget = interactable;
                    currentTarget.OnStartLook();
                }
                else
                {
                    currentTarget = interactable;
                    currentTarget.OnStartLook();
                }
            }
            // If the object hit was not interactable
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.OnEndLook();
                    currentTarget = null;
                }
            }
        }
        // If the raycast hit nothing
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndLook();
                currentTarget = null;
            }
        }
    }
}
