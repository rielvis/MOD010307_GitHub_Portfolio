using MicrogameCourse.Microgames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int dataValue;
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

        // Gets the integer data value for the column based on its name
        for (int i = 1; i < 6; i++)
        {
            if (gameObject.name == $"Col{i}")
            {
                dataValue = i;
            }
        }
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
        gameManager.CheckColumnOrder();
    }

    private void ChangePosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        touchedWorldPosition = cam.ScreenToWorldPoint(touchedScreenPosition);

        gameManager.emptySlot = slot; // Gets the current slot and assigns it to the emptySlot variable in MG4GameManager
        transform.position = new Vector3(touchedWorldPosition.x, transform.position.y, 0); // Moves to the new position
        gameManager.ColumnSlotAssignment(gameObject); // Checks if that position warrants a column-slot reassignment, and if so reassigns it, where then positioning is handled independently in each column script
    }

    private void SnapPosition()
    {
        transform.position = new Vector2(slot.transform.position.x, transform.position.y);
    }
}
