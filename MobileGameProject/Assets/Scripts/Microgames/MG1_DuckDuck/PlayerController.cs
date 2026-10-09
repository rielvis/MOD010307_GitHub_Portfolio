using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D pRigidbody;

    [Header("Player Velocity")]
    [SerializeField] private int maximumPlayerSpeed;
    [SerializeField] private float pLinearVelocityX;
    [SerializeField] private float pLinearVelocityY;

    void Start()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        maximumPlayerSpeed = 3;

        RandomisePlayerVelocity();
        pRigidbody.linearVelocity = new UnityEngine.Vector2(pLinearVelocityX, pLinearVelocityY);
    }

    void Update()
    {
        pRigidbody.linearVelocity = new UnityEngine.Vector2(pLinearVelocityX, pLinearVelocityY);
    }

    private void RandomisePlayerVelocity()
    {
        if (pLinearVelocityX != 0 || pLinearVelocityY != 0) return;
        
        pLinearVelocityX = Random.Range(1, maximumPlayerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
        pLinearVelocityY = Random.Range(1, maximumPlayerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
            // Random.Range(1, 4) returns an integer between 1 and the max set speed.
            // R.R(0, 2) produces either 0 or 1, ternery then makes the speed negative or positive.
            // This sets a random direction for the player to start moving in.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collided with " + collision.gameObject.name);

        if (collision.gameObject.name == "bottomWall" || collision.gameObject.name == "topWall")
        {
            pLinearVelocityY = pLinearVelocityY * -1;
            // Flips the negativity/positivity of the value, thereby inverting the direction.
            // Multiplying any number by -1 flips the negativity/positivity of it.
        }

        if (collision.gameObject.name == "leftWall" || collision.gameObject.name == "rightWall")
        {
            pLinearVelocityX = pLinearVelocityX * -1;
        }
    }
}
