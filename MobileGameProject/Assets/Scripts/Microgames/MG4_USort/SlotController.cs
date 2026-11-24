using UnityEngine;

public class SlotController : MonoBehaviour
{
    public GameObject column;
    public int dataValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the integer data value for the column based on its name
        for (int i = 1; i < 6; i++)
        {
            if (gameObject.name == $"Slot{i}")
            {
                dataValue = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
