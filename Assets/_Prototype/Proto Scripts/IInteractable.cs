using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Is called whenever the player looks at the object.
    /// </summary>
    void OnStartLook();

    /// <summary>
    /// Is called whenever the player presses interact on the object.
    /// </summary>
    void OnInteract();

    /// <summary>
    /// Is called whenever the player stops looking at the object.
    /// </summary>
    void OnEndLook();
}
