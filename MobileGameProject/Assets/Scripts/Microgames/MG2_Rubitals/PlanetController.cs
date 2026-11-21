using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Be able to touch the planet objects and update their position according to where we touch on the screen.
/// </summary>

public class PlanetController : MonoBehaviour
{
    public InputActionAsset InputActions;
    public InputAction debugButton;
    private Camera cam;

    private Vector3 touchedScreenPosition;
    private bool isDragging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debugButton = InputSystem.actions.FindAction("Debug");

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /// Check where we are touch the screen and get the world position from that.
        /// Update planet position to that world position derived.
        //transform.position = touchedPosition;

        if (debugButton.WasPressedThisFrame())
        {
            // Do debug stuff
        }

        if (Pointer.current.press.wasPressedThisFrame)
        {
            isDragging = true;
        }
        else if (Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            GetTouchedPosition();
            ChangePosition();
        }
    }

    private void GetTouchedPosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        Debug.Log(touchedScreenPosition);
    }

    private void ChangePosition()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(touchedScreenPosition.x, touchedScreenPosition.y, 0)); 
            // Still results in position.z being set to -10...
            // Manually setting below...
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
