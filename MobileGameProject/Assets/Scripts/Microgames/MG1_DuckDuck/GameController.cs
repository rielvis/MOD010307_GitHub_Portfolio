using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MicrogameBehaviour
{

    /// <summary>
    /// Game states, hunter spawning, waves/rounds measured as "hunter ammo"
    /// where the round beings when they "reload" and finishes when they run out of ammo.
    /// </summary>
    
    public Transform playerTransform;
    
    [SerializeField] private float playAreaLimit = 1.5f;

    [SerializeField] public GameObject hunterPrefab;
    [SerializeField] private int spawnRate; // How many will spawn per second

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnHunter() // Run this however often your spawn rate is.
    {
        // Vector3 targetPosition;
        // float randPosX;
        // float randPosY;

        // if (Random.Range(0,2) == 0)
        //     targetPosition = playerTransform.position;
        // else if (Random.Range(0,2) == 1)
        //     targetPosition = new Vector3(randPosX, randPosY, 0f);

        // Instantiate(hunterPrefab, targetPosition, Quaternion.identity);
    }

    private void SetHunterRandomPosition()
    {
        playAreaLimit = 2f;

        float posX = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);
        float posY = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);

        transform.position = new Vector3(posX, posY, 0f);
    }
}
