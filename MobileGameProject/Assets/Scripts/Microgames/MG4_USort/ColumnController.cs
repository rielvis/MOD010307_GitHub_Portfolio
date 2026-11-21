using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging;
    private Vector2 touchedScreenPosition;
    private Vector2 touchedWorldPosition;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            if (touchedWorldPosition.y >= slot1.transform.position.y && touchedWorldPosition.y <= slot5.transform.position.y)
            {
                GetTouchedPosition();
                ChangePosition();
            }
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isDragging = false;
    }

    private void GetTouchedPosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        touchedWorldPosition = cam.ScreenToWorldPoint(touchedScreenPosition);
    }

    private void ChangePosition()
    {
        transform.position = new Vector3(touchedWorldPosition.x, transform.position.y, 0);
    }
}
