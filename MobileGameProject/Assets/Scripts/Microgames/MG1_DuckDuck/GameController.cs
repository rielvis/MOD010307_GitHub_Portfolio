using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    public class GameController : MicrogameBehaviour
    {
        /// <summary>
        /// Game states, hunter spawning, waves/rounds measured as "hunter ammo"
        /// where the round beings when they "reload" and finishes when they run out of ammo.
        /// </summary>
        [SerializeField] public GameObject hunterPrefab;
        [SerializeField] private float maxHunterSpawnTimerDuration;
        
        private float hunterSpawnTimer;
        public int hunterCount;
        public Text remainingHunterText;

        public bool isPlayerHit;

        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            hunterSpawnTimer = maxHunterSpawnTimerDuration;
            SetRemainingHunterText();
        }

        void Update()
        {
            if (!IsRunning) return;

            HunterSpawnCycle();

            if (hunterCount <= 0)
            {
                Win();
            }
            else if (isPlayerHit)
            {
                Lose();
            }
        }

        public void SpawnHunter()
        {
            Instantiate(hunterPrefab, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
        }

        private void HunterSpawnCycle()
        {
            hunterSpawnTimer -= Time.deltaTime;

            if (hunterSpawnTimer <= 0 && hunterCount > 0)
            {
                Instantiate(hunterPrefab, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
                hunterSpawnTimer = maxHunterSpawnTimerDuration;
            }
        }

        public void SetRemainingHunterText()
        {
            remainingHunterText.text = $"Hunters Remaining: {hunterCount}";
        }
    }
}

