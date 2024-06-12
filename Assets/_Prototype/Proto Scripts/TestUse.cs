using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUse : MonoBehaviour, IInteractable
{
    public void OnEndLook()
    {
        EventBus.Instance.EndInteractableLook();
    }

    public void OnInteract()
    {
        Debug.Log("Interacting with: " + gameObject.name);
    }

    public void OnStartLook()
    {
        EventBus.Instance.StartInteractableLook();
    }
}
