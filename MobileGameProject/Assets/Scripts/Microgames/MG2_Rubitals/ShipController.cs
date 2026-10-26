using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Randomise rotation within a certain range
        SetRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetRandomRotation()
    {
        // transform.rotation = Quaternion.Euler(0, 0, RandomRange(0, 46));
    }

    public void LaunchShip()
    {
        rb.linearVelocityY = 3;
    }
}
