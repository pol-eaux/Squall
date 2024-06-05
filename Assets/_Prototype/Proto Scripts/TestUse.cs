using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUse : MonoBehaviour, IInteractable
{
    public void OnEndLook()
    {
        Debug.Log("Stopped looking at: " + gameObject.name);
    }

    public void OnInteract()
    {
        Debug.Log("Interacting with: " + gameObject.name);
    }

    public void OnStartLook()
    {
        Debug.Log("Looking at: " + gameObject.name);
    }
}
