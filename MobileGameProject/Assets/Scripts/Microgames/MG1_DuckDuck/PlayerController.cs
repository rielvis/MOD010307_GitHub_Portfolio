using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D pRigidbody;

    [Header("Player Velocity")]
    [SerializeField] private int playerSpeed;
    [SerializeField] private float pLinearVelocityX;
    [SerializeField] private float pLinearVelocityY;

    void Start()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        playerSpeed = 3;

        RandomisePlayerVelocity();
        pRigidbody.linearVelocity = new UnityEngine.Vector2(pLinearVelocityX, pLinearVelocityY);
    }

    void Update()
    {
        // When the ball hits the side of the screen, reverse the direction.
            // How to detect the side?
            // How to reverse direction: negative velocity? changing transform?
    }

    private void RandomisePlayerVelocity()
    {
        if (pLinearVelocityX != 0 || pLinearVelocityY != 0) return;
        
        pLinearVelocityX = Random.Range(1, playerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
        pLinearVelocityY = Random.Range(1, playerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
            // Random.Range(1, 4) returns an integer between 1 and the max set speed.
            // R.R(0, 2) produces either 0 or 1, ternery then makes the speed negative or positive.
            // This sets a random direction for the player to start moving in.
    }
}
