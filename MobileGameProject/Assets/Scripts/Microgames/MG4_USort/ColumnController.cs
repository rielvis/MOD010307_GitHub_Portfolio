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
    public enum Slot {s1, s2, s3, s4, s5}
    public Slot slotPlace;
    
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
            GetTouchedPosition();
            ChangePosition();
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

    private void GetTouchedPosition()
    {
        touchedScreenPosition = Pointer.current.position.ReadValue();
        touchedWorldPosition = cam.ScreenToWorldPoint(touchedScreenPosition);
    }

    private void ChangePosition()
    {
        transform.position = new Vector3(touchedWorldPosition.x, transform.position.y, 0);
        GetSnapPosition();
    }

    private void SnapPosition()
    {
        transform.position = new Vector2(snapPosX, transform.position.y);
    }

    private void GetSnapPosition()
    {
        ownPosX = transform.position.x;
        float s1s2MidPoint = (gameManager.s1posX + gameManager.s2posX) / 2;
        float s2s3MidPoint = (gameManager.s2posX + gameManager.s3posX) / 2;
        float s3s4MidPoint = (gameManager.s3posX + gameManager.s4posX) / 2;
        float s4s5MidPoint = (gameManager.s4posX + gameManager.s5posX) / 2;

        if (ownPosX < s1s2MidPoint) // If the column position is less than the mid point between Slot1 and Slot2
        {
            snapPosX = gameManager.s1posX;
            slotPlace = Slot.s1;
        }
        else if (ownPosX > s1s2MidPoint && ownPosX < s2s3MidPoint)
        {
            snapPosX = gameManager.s2posX;
            slotPlace = Slot.s2;
        }
        else if (ownPosX > s2s3MidPoint && ownPosX < s3s4MidPoint)
        {
            snapPosX = gameManager.s3posX;
            slotPlace = Slot.s3;
        }
        else if (ownPosX > s3s4MidPoint && ownPosX < s4s5MidPoint)
        {
            snapPosX = gameManager.s4posX;
            slotPlace = Slot.s4;
        }
        else if (ownPosX > s4s5MidPoint)
        {
            snapPosX = gameManager.s5posX;
            slotPlace = Slot.s5;
        }
    }

    private void SetSlotPosition()
    {
        
    }
}
