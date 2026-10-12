using UnityEngine;

// There will be multiple "Hunter" objects that spawn in this scene. They are target areas that off-screen hunters are aiming at.
// They should spawn at a random location within the play area. Shortly after, they should trigger a "shot".
// If the player is caught in their area, the player dies and the game is over.

public class HunterController : MonoBehaviour
{
    [SerializeField] private float playAreaLimit;
    [SerializeField] private float sizeLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set a random position within the play area.
        SetRandomPosition();

        // Begin a countdown to the "shot".
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in the trigger area during the "shot", the game is over.
    }

    private void SetRandomPosition()
    {
        playAreaLimit = 2f;

        float posX = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);
        float posY = Random.Range(0, playAreaLimit) * (Random.Range(0, 2) == 0 ? -1 : 1);

        transform.position = new Vector3(posX, posY, 0f);
    }

    private void SetRandomSize()
    {
        sizeLimit = 1;

        float scaleX = Random.Range(0.5f, sizeLimit);
        float scaleY = Random.Range(0.5f, sizeLimit);
        float scaleZ = Random.Range(0.5f, sizeLimit);

        // transform.scale = new Vector3(scaleX, scaleY, scaleZ);

        return;
    }
}
