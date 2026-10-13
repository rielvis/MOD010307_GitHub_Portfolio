using UnityEngine;

// There will be multiple "Hunter" objects that spawn in this scene. They are target areas that off-screen hunters are aiming at.
// They should spawn at a random location within the play area. Shortly after, they should trigger a "shot".
// If the player is caught in their area, the player dies and the game is over.

public class HunterController : MonoBehaviour
{
    [SerializeField] private float playAreaLimit;
    [SerializeField] private float sizeLimit;

    private float shotTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Begin a countdown to the "shot".
        SetTimer(11);

        // Set a random position within the play area.
        SetRandomPosition();
        SetRandomSize();


    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
    }

    private void SetRandomPosition()
    {
        playAreaLimit = 2f;

        float posX = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);
        float posY = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);

        transform.position = new Vector3(posX, posY, 0f);
    }

    private void SetRandomSize()
    {
        sizeLimit = 1;

        float scaleV = Random.Range(0.5f, sizeLimit);

        transform.localScale = new Vector3(scaleV, scaleV, scaleV);
    }

    private void SetTimer(float time)
    {
        shotTimer = time;
    }

    private void RunTimer()
    {
        if (shotTimer >= 1f)
        {  
            shotTimer -= Time.deltaTime;
            Debug.Log($"Time Remaining: {(int)shotTimer}");
        }
    }

    private void OnTriggerEnter2D(Collider2D area)
    {
        // If the player is in the trigger area during the "shot", the game is over.
    }
}
