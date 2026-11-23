using MicrogameCourse.Microgames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float ownPosX;
    private bool isDragging;
    private Vector2 touchedScreenPosition;
    private Vector2 touchedWorldPosition;
    public Camera cam;
    public MG4GameManager gameManager;

    [Header("Slots")]
    private float snapPosX;
    public GameObject slot;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
        cam = Camera.main;
        gameManager = GameObject.Find("GameManager").GetComponent<MG4GameManager>();

        ownPosX = transform.position.x;

        GetSlotSnapPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            GetTouchedPosition();
            ChangePosition();
        }
        else
        {
            GetSlotSnapPosition();
            SetPosition();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isDragging = false;
        SnapPosition();
    }

    public void SetPosition()
    {
        
    }

    private void GetTouchedPosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        touchedWorldPosition = cam.ScreenToWorldPoint(touchedScreenPosition);
    }

    private void ChangePosition()
    {
        transform.position = new Vector3(touchedWorldPosition.x, transform.position.y, 0);
        GetSlotSnapPosition();
    }

    private void SnapPosition()
    {
        transform.position = new Vector2(snapPosX, transform.position.y);
    }

    private void GetSlotSnapPosition()
    {
        ownPosX = transform.position.x;
        float s1s2MidPoint = (gameManager.slotsPosX[0] + gameManager.slotsPosX[1]) / 2;
        float s2s3MidPoint = (gameManager.slotsPosX[1] + gameManager.slotsPosX[2]) / 2;
        float s3s4MidPoint = (gameManager.slotsPosX[2] + gameManager.slotsPosX[3]) / 2;
        float s4s5MidPoint = (gameManager.slotsPosX[3] + gameManager.slotsPosX[4]) / 2;

        if (ownPosX < s1s2MidPoint) // If the column position is less than the mid point between Slot1 and Slot2
        {
            snapPosX = gameManager.slotsPosX[0];
            slot = gameManager.slots[0];
            slot.GetComponent<SlotController>().column = gameObject;
        }
        else if (ownPosX > s1s2MidPoint && ownPosX < s2s3MidPoint)
        {
            snapPosX = gameManager.slotsPosX[1];
            slot = gameManager.slots[1];
            slot.GetComponent<SlotController>().column = gameObject;
        }
        else if (ownPosX > s2s3MidPoint && ownPosX < s3s4MidPoint)
        {
            snapPosX = gameManager.slotsPosX[2];
            slot = gameManager.slots[2];
            slot.GetComponent<SlotController>().column = gameObject;
        }
        else if (ownPosX > s3s4MidPoint && ownPosX < s4s5MidPoint)
        {
            snapPosX = gameManager.slotsPosX[3];
            slot = gameManager.slots[3];
            slot.GetComponent<SlotController>().column = gameObject;

        }
        else if (ownPosX > s4s5MidPoint)
        {
            snapPosX = gameManager.slotsPosX[4];
            slot = gameManager.slots[4];
            slot.GetComponent<SlotController>().column = gameObject;
        }
    }
}
