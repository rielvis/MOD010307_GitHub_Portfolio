using UnityEngine;

// There will be multiple "Hunter" objects that spawn in this scene. They are target areas that off-screen hunters are aiming at.
// They should spawn at a random location within the play area. Shortly after, they should trigger a "shot".
// If the player is caught in their area, the player dies and the game is over.

public class HunterController : MonoBehaviour
{
    [SerializeField] private float playAreaLimit;
    [SerializeField] private float sizeLimit;
    [SerializeField, Min(3)] private float shotTime;
    private float shotTimer;

    public GameObject gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assign gameManager object to GameObject variable

        // Begin a countdown to the "shot".
        SetTimer(shotTime);

        // Set a random position within the play area.
        SetRandomSize(10);
        SetRandomPosition();
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

    private void SetRandomSize(float size)
    {
        sizeLimit = size;

        float scaleV = Random.Range(0.5f, sizeLimit);

        transform.localScale = new Vector3(scaleV, scaleV, scaleV);
    }

    private void SetTimer(float time)
    {
        shotTimer = time;
    }

    private void RunTimer()
    {
        if ((int)shotTimer >= 1)
        {  
            shotTimer -= Time.deltaTime;
            Debug.Log($"Time Remaining: {(int)shotTimer}");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the player is in the trigger area during the "shot", the game is over.
        // The shot happens when the shot timer reaches 0.
        if ((int)shotTimer <= 0 && collision.gameObject.name == "Player")
        {
            // set game over in gameManager
            Debug.Log("Player is hit!");
        }
    }
}
