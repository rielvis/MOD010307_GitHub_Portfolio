using MicrogameCourse.Framework;
using UnityEngine;

namespace MicrogameCourse.Microgames
{
    public class MG3GameManager : MicrogameBehaviour
    {
        [SerializeField] public Camera cam;
        [SerializeField] private float timerDuration;
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
            targetSpawnTimer = 0;
        }

        void Update()
        {
            if (IsRunning == true)
            {
                SpawnTargets();
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

        private void SpawnTargets()
        {
            if (targetSpawnTimer > 0)
            {
                targetSpawnTimer -= Time.deltaTime;
            }
            else if (targetSpawnTimer <= 0)
            {
                Instantiate(targetPrefab, new Vector3(Random.Range(-2f, 2f), Random.Range(-4f, 3f), 0), Quaternion.identity);
                targetSpawnTimer = timerDuration;
            }
        }
    }
}
