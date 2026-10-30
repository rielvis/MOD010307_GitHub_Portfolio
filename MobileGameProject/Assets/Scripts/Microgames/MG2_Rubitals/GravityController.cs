using MicrogameCourse.Framework;
using MicrogameCourse.Microgames;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        // pull the physics object in this trigger field towards the center
        Debug.Log("Pulling on object!");
    }
}
