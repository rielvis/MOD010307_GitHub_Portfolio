using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MicrogameCourse.Microgames
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public GameController gameManager;

        [Header("Player Input")]
        [SerializeField] public InputActionAsset InputActions;
        private InputAction dartUp;
        private InputAction dartDown;
        private InputAction dartLeft;
        private InputAction dartRight;
        private InputAction hunterSpawn;

        [Header("Player Physics, Speed, and Velocity")]
        [SerializeField] private Rigidbody2D pRigidbody;
        [SerializeField] private int maximumPlayerSpeed;
        [SerializeField] private float pLinearVelocityX;
        [SerializeField] private float pLinearVelocityY;
        [SerializeField] private int playerDartForce;

        private void Awake()
        {
            dartUp = InputSystem.actions.FindAction("Up");
            dartDown = InputSystem.actions.FindAction("Down");
            dartLeft = InputSystem.actions.FindAction("Left");
            dartRight = InputSystem.actions.FindAction("Right");
            hunterSpawn = InputSystem.actions.FindAction("Spawn");
        }

        void Start()
        {
            pRigidbody = GetComponent<Rigidbody2D>();
            maximumPlayerSpeed = 3;
            playerDartForce = 1;

            RandomisePlayerVelocity();
            SetPlayerSpeed();
        }

        void Update()
        {
            SetPlayerSpeed();
            DartPlayer();

            if (hunterSpawn.WasPressedThisFrame())
            {
                gameManager.SpawnHunter();
            }
        }

        private void RandomisePlayerVelocity()
        {
            if (pLinearVelocityX != 0 || pLinearVelocityY != 0) return;

            pLinearVelocityX = Random.Range(1, maximumPlayerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
            pLinearVelocityY = Random.Range(1, maximumPlayerSpeed + 1) * (Random.Range(0, 2) == 0 ? -1 : 1);
        }

        private void SetPlayerSpeed()
        {
            pRigidbody.linearVelocity = new UnityEngine.Vector2(pLinearVelocityX, pLinearVelocityY);

        }

        private void DartPlayer() // Should refactor this, comparatives with negatives and positives are difficult to comprehend...
        {
            if (dartUp.WasPressedThisFrame())
            {
                if (pRigidbody.linearVelocityY < 0) pLinearVelocityY = +playerDartForce;
                else if (pRigidbody.linearVelocityY <= +maximumPlayerSpeed)
                {
                    pLinearVelocityY += playerDartForce;
                }

            }
            if (dartDown.WasPressedThisFrame())
            {
                if (pRigidbody.linearVelocityY > 0) pLinearVelocityY = -playerDartForce;
                else if (pRigidbody.linearVelocityY >= -maximumPlayerSpeed)
                {
                    pLinearVelocityY -= playerDartForce;
                }
            }
            if (dartLeft.WasPressedThisFrame())
            {
                if (pRigidbody.linearVelocityX > 0) pLinearVelocityX = -playerDartForce;
                else if (pRigidbody.linearVelocityX >= -maximumPlayerSpeed)
                {
                    pLinearVelocityX -= playerDartForce;
                }
            }
            if (dartRight.WasPressedThisFrame())
            {
                if (pRigidbody.linearVelocityX < 0) pLinearVelocityX = +playerDartForce;
                else if (pRigidbody.linearVelocityX <= +maximumPlayerSpeed)
                {
                    pLinearVelocityX += playerDartForce;
                }
            }
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

}
