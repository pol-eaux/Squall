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
        EventBus.Instance.OnStartLook += ShowInteractableCrosshair;
        EventBus.Instance.OnEndLook += ShowDefaultCrosshair;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnStartLook -= ShowInteractableCrosshair;
        EventBus.Instance.OnEndLook -= ShowDefaultCrosshair;
    }

    private void Awake()
    {
        hudCanvas.enabled = true;
        ShowDefaultCrosshair();
    }

    private void ShowInteractableCrosshair()
    {
        defaultCrosshair.enabled = false;
        interactCrosshair.enabled = true;
    }

    private void ShowDefaultCrosshair()
    {
        defaultCrosshair.enabled = true;
        interactCrosshair.enabled = false;
    }
}
