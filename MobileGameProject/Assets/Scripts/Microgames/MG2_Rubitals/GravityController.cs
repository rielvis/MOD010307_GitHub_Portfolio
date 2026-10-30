using MicrogameCourse.Framework;
using MicrogameCourse.Microgames;
using Unity.VisualScripting;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private Vector3 objVector;
    private float distanceToCore;
    [SerializeField] private float gravityStrength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityStrength = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pull the physics object in this trigger field towards the center.
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Pulling on object!");

        //GetObjectVector
        objVector = transform.position - collider.transform.position;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        objVector = transform.position - collider.transform.position;
        distanceToCore = (objVector.x + objVector.y) / 2;


        collider.attachedRigidbody.AddForce(objVector.normalized * 10, ForceMode2D.Force);
    }
}
