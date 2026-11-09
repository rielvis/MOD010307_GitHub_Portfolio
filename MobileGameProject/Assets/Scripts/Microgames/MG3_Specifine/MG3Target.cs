using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class MG3Target : MonoBehaviour
{
    public enum TargetType
    {
        Red,
        Green
    }

    private TargetType targetType;

    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetTargetAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTargetAttributes()
    {
        // Two possible states for target, tenery statement to randomly pick a state upon instantiation
        targetType = UnityEngine.Random.Range(0, 2) == 0 ? TargetType.Red : TargetType.Green ;

        if (targetType == TargetType.Red) { sr.color = new Color(255, 0, 0, 255); }
        else if (targetType == TargetType.Green) {sr.color = new Color(255, 0, 0, 255); }
        else {sr.color = new Color(255, 255, 255, 255); }
    }
}
