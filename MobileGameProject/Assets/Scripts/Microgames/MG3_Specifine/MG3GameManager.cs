using MicrogameCourse.Framework;
using UnityEngine;

namespace MicrogameCourse.Microgames
{
    public class MG3GameManager : MicrogameBehaviour
    {
        [SerializeField] public Camera cam;
        [SerializeField] private float maxTimerDuration;
        [SerializeField] private GameObject targetPrefab;
        private float targetSpawnTimer;

        public enum TypeState
        {
            Red,
            Green
        }

        private TypeState playerState;

        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            SetPlayerState();
            targetSpawnTimer = maxTimerDuration / 2;
        }

        void Update()
        {
            if (IsRunning == true)
            {
                SpawnTargets(1);
            }
        }

        private void SetPlayerState()
        {
            playerState = Random.Range(0, 2) == 0 ? TypeState.Red : TypeState.Green;
            Debug.Log("Player state is " + playerState);

            if (playerState == TypeState.Red) { cam.backgroundColor = new Color(255, 0, 0, 255); }
            else if (playerState == TypeState.Green) { cam.backgroundColor = new Color(0, 255, 0, 255); }
            else { cam.backgroundColor = new Color(0, 0, 0, 255); }
        }

        private void SpawnTargets(int amount)
        {
            if (targetSpawnTimer > 0)
            {
                targetSpawnTimer -= Time.deltaTime;
            }
            else if (targetSpawnTimer <= 0)
            {
                for (int i = 1; i <= amount; i++)
                {
                    Instantiate(targetPrefab, new Vector3(Random.Range(-2f, 2f), Random.Range(-4f, 3f), 0), Quaternion.identity);
                }
                targetSpawnTimer = maxTimerDuration - Random.Range(0f, maxTimerDuration);
            }
        }
    }
}
