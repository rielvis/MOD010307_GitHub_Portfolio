using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MG3Target : MonoBehaviour
{
    public enum TargetType
    {
        Red,
        Green
    }

    private TargetType targetType;

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
        // Two possible states for target, tenery statement to randomly pick a state upon instantiation
        targetType = UnityEngine.Random.Range(0, 2) == 0 ? TargetType.Red : TargetType.Green ;

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
    }

    private void RunLife()
    {
        if (targetLifeTimer > 0)
        {
            targetLifeTimer -= Time.deltaTime;
            Debug.Log(targetLifeTimer);   
        }
        
        if (targetLifeTimer < (lifeDuration / 2))
        {
            targetColor.a = 0.1f;
            sr.color = targetColor;
        }
        
        if (targetLifeTimer <= 0f)
        {
            Debug.Log("Target gone!");
            Destroy(gameObject, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D area)
    {
        if (area.name == "Area")
        {
            Destroy(gameObject);
        }
    }
}
