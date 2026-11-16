using System.Threading;
using UnityEngine;

// There will be multiple "Hunter" objects that spawn in this scene. They are target areas that off-screen hunters are aiming at.
// They should spawn at a random location within the play area. Shortly after, they should trigger a "shot".
// If the player is caught in their area, the player dies and the game is over.

namespace MicrogameCourse.Microgames
{
        public class HunterController : MonoBehaviour
    {
        [Header("Player Variables")]
        private bool isPlayerSeen = false;

        [Header("Timer")]
        [SerializeField, Min(3)] private float shotTime;
        private float shotTimer;

        [Header("Other")]
        [SerializeField] private float sizeLimit;

        public GameController gameManager;
        private GameObject player;

        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameController>();

            // Begin a countdown to the "shot".
            shotTime = 5;
            SetTimer(shotTime);

            // Set a random position within the play area.
            SetRandomSize(1, 3);
        }

        void Update()
        {
            RunTimer();
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
            gameManager.hunterCount -= 1;
            gameManager.SetRemainingHunterText();

            if (isPlayerSeen)
            {
                Debug.Log("Player was hit!");
                gameManager.isPlayerHit = true;
                Destroy(gameObject);
            }
            else if (!isPlayerSeen)
            {
                //Debug.Log("Player was not hit!");
                gameManager.isPlayerHit = false;
                Destroy(gameObject);
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
            if (collision.gameObject.name == "Player" && gameManager.isPlayerHit == false)
            {
                isPlayerSeen = false;
                //Debug.Log("Player lost!");
            }
        }
    }
}

