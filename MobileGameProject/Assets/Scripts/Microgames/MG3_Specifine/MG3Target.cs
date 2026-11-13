using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Rendering;

public class MG3Target : MonoBehaviour
{
    public enum TargetType
    {
        Red,
        Green
    }

    private enum SpawnType
    {
        Left,
        Right
    }

    private TargetType targetType;
    private SpawnType spawnType;
    [SerializeField] private float targetMoveSpeed;

    private SpriteRenderer sr;
    private Color targetColor;

    [SerializeField] private float lifeDuration;
    private float targetLifeTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetColor = sr.color;
        SetTargetAttributes();
        targetLifeTimer = lifeDuration;

        GraphicsSettings.useScriptableRenderPipelineBatching = false; // This makes alpha channel changes show during runtime (f u Unity)
    }

    // Update is called once per frame
    void Update()
    {
        RunLife();
    }

    private void SetTargetAttributes()
    {
        // Two possible states for target type, tenery statement to randomly pick a state upon instantiation
        targetType = UnityEngine.Random.Range(0, 2) == 0 ? TargetType.Red : TargetType.Green;

        if (targetType == TargetType.Red)
        { 
            sr.color = new Color(255, 0, 0, 255); 
        }
        else if (targetType == TargetType.Green)
        {
            sr.color = new Color(0, 255, 0, 255);
        }
        else 
        {
            sr.color = new Color(255, 255, 255, 255);
        }

        targetColor = sr.color;

        // Setting the spawn type, whether it spawns from the left or right of the screen
        spawnType = UnityEngine.Random.Range(0, 2) == 0 ? SpawnType.Left : SpawnType.Right;

        if (spawnType == SpawnType.Left)
        {
            transform.position = new Vector3(-4f, UnityEngine.Random.Range(-4f, 3f), 0);
        }
        else if (spawnType == SpawnType.Right)
        {
            transform.position = new Vector3(4f, UnityEngine.Random.Range(-4f, 3f), 0);
        }
        else
        {
            return;
        }
    }

    private void RunLife()
    {
        // If the targets spawn at random locations and fade away in place
            // if (targetLifeTimer > 0)
            // {
            //     targetLifeTimer -= Time.deltaTime;
            //     //Debug.Log(targetLifeTimer);   
            // }
            
            // if (targetLifeTimer < (lifeDuration / 2))
            // {
            //     targetColor.a = 0.1f;
            //     sr.color = targetColor;
            // }
            
            // if (targetLifeTimer <= 0f)
            // {
            //     Debug.Log("Target gone!");
            //     Destroy(gameObject, 0f);
            // }
        
        // If the targets spawn off screen and move across the screen, destroying once they exit the screen
        if (spawnType == SpawnType.Left)
        {
            transform.position = new Vector3(transform.position.x + (targetMoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        else if (spawnType == SpawnType.Right)
        {
            transform.position = new Vector3(transform.position.x + (-targetMoveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (transform.position.x >= 5 || transform.position.x <= -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D area)
    {
        if (area.name == "Area")
        {
            // Destroy(gameObject); // not working?? object does not destroy when leaving the area
            return;
        }
    }
}
