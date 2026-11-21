using System;
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

    [Header("Slots")]
    public GameObject slot1;
    private float s1posX;
    public GameObject slot2;
    private float s2posX;
    public GameObject slot3;
    private float s3posX;
    public GameObject slot4;
    private float s4posX;
    public GameObject slot5;
    private float s5posX;
    private float snapPosX;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
        cam = Camera.main;

        ownPosX = transform.position.x;
        s1posX = slot1.transform.position.x;
        s2posX = slot2.transform.position.x;
        s3posX = slot3.transform.position.x;
        s4posX = slot4.transform.position.x;
        s5posX = slot5.transform.position.x;
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
    }

    private void SnapPosition()
    {
        GetSnapPosition();
        transform.position = new Vector2(snapPosX, transform.position.y);
    }

    private void GetSnapPosition()
    {
        ownPosX = transform.position.x;
        float s1s2MidPoint = (s1posX + s2posX) / 2;
        float s2s3MidPoint = (s2posX + s3posX) / 2;
        float s3s4MidPoint = (s3posX + s4posX) / 2;
        float s4s5MidPoint = (s4posX + s5posX) / 2;

        if (ownPosX < s1s2MidPoint) // If the column position is less than the mid point between Slot1 and Slot2
        {
            snapPosX = s1posX;
        }
        else if (ownPosX > s1s2MidPoint && ownPosX < s2s3MidPoint)
        {
            snapPosX = s2posX;
        }
        else if (ownPosX > s2s3MidPoint && ownPosX < s3s4MidPoint)
        {
            snapPosX = s3posX;
        }
        else if (ownPosX > s3s4MidPoint && ownPosX < s4s5MidPoint)
        {
            snapPosX = s4posX;
        }
        else if (ownPosX > s4s5MidPoint)
        {
            snapPosX = s5posX;
        }
    }
}
