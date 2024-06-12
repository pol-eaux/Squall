using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public event Action OnStartLook;
    public event Action OnEndLook;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Invokes the on start look event.
    /// </summary>
    public void StartInteractableLook()
    {
        OnStartLook?.Invoke();
    }

    /// <summary>
    /// Invokes the on end look event.
    /// </summary>
    public void EndInteractableLook()
    {
        OnEndLook?.Invoke();
    }
}
