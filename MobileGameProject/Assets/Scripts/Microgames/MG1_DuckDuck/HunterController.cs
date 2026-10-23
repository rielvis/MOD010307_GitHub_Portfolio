using System.Threading;
using UnityEngine;

// There will be multiple "Hunter" objects that spawn in this scene. They are target areas that off-screen hunters are aiming at.
// They should spawn at a random location within the play area. Shortly after, they should trigger a "shot".
// If the player is caught in their area, the player dies and the game is over.

public class HunterController : MonoBehaviour
{
    [Header("Player Variables")]
    private bool isPlayerSeen = false;
    private bool isPlayerHit;

    [Header("Timer")]
    [SerializeField, Min(3)] private float shotTime;
    private float shotTimer;

    [Header("Other")]
    [SerializeField] private float sizeLimit;

    public GameObject gameManager;
    private GameObject player;

    void Start()
    {
        // Begin a countdown to the "shot".
        shotTime = 5;
        SetTimer(shotTime);

        // Set a random position within the play area.
        SetRandomSize(1, 3);
    }

    void Update()
    {
        RunTimer();

        if (isPlayerHit)
        {
            player.SetActive(false);
        }
    }

    private void SetRandomSize(float minSize, float maxSize)
    {
        float scaleV = Random.Range(minSize, maxSize);

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
            //Debug.Log("Player was not hit!");
            isPlayerHit = false;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player = collision.gameObject;
            isPlayerSeen = true;
            //Debug.Log("Player found!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isPlayerHit == false)
        {
            isPlayerSeen = false;
            //Debug.Log("Player lost!");
        }
    }
}
