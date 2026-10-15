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
    private bool isPlayerSeen = false;
    private bool isPlayerHit;
    [SerializeField] private bool debugTimer = false;

    public GameObject gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assign gameManager object to GameObject variable

        // Begin a countdown to the "shot".
        shotTime = 5;
        SetTimer(shotTime);

        // Set a random position within the play area.
        SetRandomSize(3);
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
            if (debugTimer)
            {
                Debug.Log($"Time Remaining: {(int)shotTimer}");                
            }


        }
        else if ((int)shotTimer <= 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (isPlayerSeen)
        {
            Debug.Log("Player was hit!");
            isPlayerHit = true;
            gameObject.SetActive(false);
        }
        else if (!isPlayerSeen)
        {
            Debug.Log("Player was not hit!");
            isPlayerHit = false;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) // Unity calls this function on a trigger collider once per frame if it detects another Collider inside the trigger collider.
    {
        if (collision.gameObject.name == "Player")
        {
            isPlayerSeen = true;
            Debug.Log("Player found!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isPlayerHit == false)
        {
            isPlayerSeen = false;
            Debug.Log("Player lost!");
        }
    }
}
