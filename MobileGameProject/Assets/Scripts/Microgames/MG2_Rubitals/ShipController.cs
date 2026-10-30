using System;
using MicrogameCourse.Framework;
using MicrogameCourse.Microgames;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 launchVector;
    private float launchSpeed;

    [SerializeField] private Transform nose;
    [SerializeField] private Transform back;

    [SerializeField] public MG2Session MicrogameSession;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Randomise rotation within a certain range
        SetRandomRotation();
        launchSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        FaceVelocity();
    }

    private void SetRandomRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20, 20));
        launchVector = new Vector2(nose.position.x - back.position.x, nose.position.y - back.position.y);
        Debug.Log("Launch Vector: " + launchVector.normalized);
    }

    public void LaunchShip()
    {
        rb.linearVelocity = launchVector * launchSpeed;
    }

    private void FaceVelocity() // taken from W3:B3 tutorial
    {
        // Too slow to have a clear direction: keep the current rotation.
        if (rb.linearVelocity.sqrMagnitude < 0.01f) return;

        // Atan2 gives the angle of the velocity from the x axis. The sprite points up (90 degrees), so subtract 90.
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90f;
        Quaternion wanted = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wanted, 540f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.name == "Destination")
        {
            MicrogameSession.Finish(true);
        }

        if (trigger.name == "GravityField")
        {
            Debug.Log("Ship in gravity field!");
        }
    }
}
