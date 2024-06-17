using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Image defaultCrosshair;
    [SerializeField] private Image interactCrosshair;

    private void OnEnable()
    {
        // Subscribe to eventbus events.
        EventBus.Instance.OnStartLook += ShowInteractableCrosshair;
        EventBus.Instance.OnEndLook += ShowDefaultCrosshair;
    }

    private void OnDisable()
    {
        // Unsubscribe from eventbus events.
        EventBus.Instance.OnStartLook -= ShowInteractableCrosshair;
        EventBus.Instance.OnEndLook -= ShowDefaultCrosshair;
    }

    private void Awake()
    {
        // On start, make sure the cavas is enabled and the default crosshair shown.
        hudCanvas.enabled = true;
        ShowDefaultCrosshair();
    }

    /// <summary>
    /// Disables the default crosshair and enabels the interact crosshair.
    /// </summary>
    private void ShowInteractableCrosshair()
    {
        defaultCrosshair.enabled = false;
        interactCrosshair.enabled = true;
    }

    /// <summary>
    /// Disables the interact crosshair and enables the default crosshair.
    /// </summary>
    private void ShowDefaultCrosshair()
    {
        defaultCrosshair.enabled = true;
        interactCrosshair.enabled = false;
    }
}
