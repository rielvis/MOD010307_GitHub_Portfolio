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


    public void SpawnHunter() // Run this however often your spawn rate is.
    {
        // Instantiate(hunterPrefab, SetHunterRandomPosition(), Quaternion.identity);
        Instantiate(hunterPrefab, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
    }

    // private Vector3 SetHunterRandomPosition(Vector3 transformPosition)
    // {
    //     playAreaLimit = 2f;

    //     float posX = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);
    //     float posY = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);

    //     transformPosition = new Vector3(posX, posY, 0f);
    // }
}
