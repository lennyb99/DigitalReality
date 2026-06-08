using UnityEngine;
using UnityEngine.InputSystem;

public class MenuTogglerOpenXR : MonoBehaviour
{
    [Header("Assign Your Canvas Here")]
    public GameObject uiCanvas;

    [Header("Assign The Menu Button Action")]
    public InputActionReference menuButtonAction;

    private void OnEnable()
    {
        // Subscribe to the button press event when this object is turned on
        if (menuButtonAction != null)
            menuButtonAction.action.performed += ToggleUI;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks when this object is turned off
        if (menuButtonAction != null)
            menuButtonAction.action.performed -= ToggleUI;
    }

    private void ToggleUI(InputAction.CallbackContext context)
    {
        if (uiCanvas != null)
        {
            // Flips the active state
            uiCanvas.SetActive(!uiCanvas.activeSelf);
        }
    }
}