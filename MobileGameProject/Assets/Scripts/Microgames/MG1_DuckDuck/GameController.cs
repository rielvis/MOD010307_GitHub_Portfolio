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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsRunning) return;
        }

        public void SpawnHunter() // Run this however often your spawn rate is.
        {
            // Instantiate(hunterPrefab, SetHunterRandomPosition(), Quaternion.identity);
            Instantiate(hunterPrefab, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
        }
    }
}

