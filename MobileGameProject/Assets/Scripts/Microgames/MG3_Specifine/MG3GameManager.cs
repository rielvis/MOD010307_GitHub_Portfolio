using System;
using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    public class MG3GameManager : MicrogameBehaviour
    {
        [SerializeField] public Camera cam;
        [SerializeField] public Text gameTimerText;
        [SerializeField] public Text stateChangeTimerText;
        [SerializeField] public Text scoreText;
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private float maxSpawnTimerDuration;
        [SerializeField] private float maxPlayerStateChangeTimerDuration;
        private float targetSpawnTimer;
        public float playerStateChangeTimer;
        public float gameTimer;
        public int playerScore;
        private int scoreToWin;

        public enum TypeState
        {
            Red,
            Green
        }

        public TypeState playerState;

        void Start()
        {
            targetSpawnTimer = maxSpawnTimerDuration / 2;
            playerStateChangeTimer = maxPlayerStateChangeTimerDuration;
            gameTimer = 30;
            playerScore = 0;
            scoreToWin = 5;
        }

        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            SetPlayerState();
        }

        void Update()
        {
            if (IsRunning == true)
            {
                TargetSpawnCycle(1);
                CyclePlayerState();

                gameTimer -= Time.deltaTime;
                gameTimerText.text = $"TIME: {gameTimer:0.0}";
            }

            if (playerScore >= scoreToWin)
            {
                Win();
            }
            else if (gameTimer <= 0)
            {
                Lose();
            }
        }

        private void SetPlayerState()
        {
            playerState = UnityEngine.Random.Range(0, 2) == 0 ? TypeState.Red : TypeState.Green;
            Debug.Log("Player state is " + playerState);
            SetBackgroundColor();
        }

        private void CyclePlayerState()
        {
            playerStateChangeTimer -= Time.deltaTime;
            stateChangeTimerText.text = $"CHANGE IN: {playerStateChangeTimer:0.00}";
            //Debug.Log(playerStateChangeTimer);

            if (playerStateChangeTimer <= 1)
            {
                if (playerState == TypeState.Red)
                {
                    ChangePlayerState(TypeState.Green);
                }
                else if (playerState == TypeState.Green)
                {
                    ChangePlayerState(TypeState.Red);
                }

                playerStateChangeTimer = maxPlayerStateChangeTimerDuration;
            }
        }

        private void SetBackgroundColor()
        {
            if (playerState == TypeState.Red) { cam.backgroundColor = new Color(255, 0, 0, 255); }
            else if (playerState == TypeState.Green) { cam.backgroundColor = new Color(0, 255, 0, 255); }
            else { cam.backgroundColor = new Color(0, 0, 0, 255); }
        }

        private void TargetSpawnCycle(int amount)
        {
            if (targetSpawnTimer > 0)
            {
                targetSpawnTimer -= Time.deltaTime;
            }
            else if (targetSpawnTimer <= 0)
            {
                for (int i = 1; i <= amount; i++)
                {
                    SpawnTarget();
                }
                targetSpawnTimer = maxSpawnTimerDuration - UnityEngine.Random.Range(0f, maxSpawnTimerDuration);
            }
        }

        private void ChangePlayerState(TypeState type)
        {
            playerState = type;
            SetBackgroundColor();
        }

        private void SpawnTarget()
        {
            //Instantiate(targetPrefab, new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-4f, 3f), 0), Quaternion.identity);
            Instantiate(targetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        public void UpdateScore(bool hitCorrectTarget)
        {
            if (hitCorrectTarget)
            {
                playerScore += 1;
                scoreText.text = $"SCORE: {playerScore}";
            }
            else
            {
                playerScore -= 1;
                scoreText.text = $"SCORE: {playerScore}";
            }
        }
    }
}
