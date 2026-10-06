using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Finds the <name> component of the object this script is attached to.
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityY = -3;
            // Sets velocity to a constant by frame-by-frame updating
            // +velocityX/Y moves it right/up, -velocityX/Y moves it left/down, standard axis convention


        // When the ball hits the side of the screen, reverse the direction.
            // How to detect the side?
            // How to reverse direction: negative velocity? changing transform?
        
        
    }
}
