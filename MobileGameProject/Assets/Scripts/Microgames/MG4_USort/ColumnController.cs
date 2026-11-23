using MicrogameCourse.Microgames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float ownPosX;
    public bool isDragging;
    private Vector2 touchedScreenPosition;
    private Vector2 touchedWorldPosition;
    public Camera cam;
    public MG4GameManager gameManager;

    [Header("Slots")]
    public GameObject slot;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
        cam = Camera.main;
        gameManager = GameObject.Find("GameManager").GetComponent<MG4GameManager>();

        ownPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            ChangePosition();
        }
        else if (!isDragging)
        {
            SnapPosition();
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

    private void ChangePosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        touchedWorldPosition = cam.ScreenToWorldPoint(touchedScreenPosition);

        gameManager.emptySlot = slot; // Gets the current empty slot
        transform.position = new Vector3(touchedWorldPosition.x, transform.position.y, 0); // moves to a new position
        gameManager.ColumnSlotAssignment(gameObject); // checks if that position warrants a column-slot reassignment, and if so reassigns it, where then positioning is handled independently in each column script
    }

    private void SnapPosition()
    {
        transform.position = new Vector2(slot.transform.position.x, transform.position.y);
    }
}
