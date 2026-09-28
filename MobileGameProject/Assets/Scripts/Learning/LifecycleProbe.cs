using UnityEngine;

namespace MicrogameCourse.Learning
{
    /// <summary>
    /// Week 2 learning tool. Attach to any GameObject to log when Unity calls
    /// each lifecycle event. The number in square brackets is the frame count.
    /// </summary>
    public class LifecycleProbe : MonoBehaviour
    {
        private bool hasLoggedFirstUpdate;

        private void Awake()
        {
            Log("Awake");
        }

        private void OnEnable()
        {
            Log("OnEnable");
        }

        private void Start()
        {
            Log("Start");
        }

        private void Update()
        {
            // Update runs every frame, so only log the first one.
            if (!hasLoggedFirstUpdate)
            {
                Log("first Update");
                hasLoggedFirstUpdate = true;
            }
        }

        private void OnDisable()
        {
            Log("OnDisable");
        }

        private void OnDestroy()
        {
            Log("OnDestroy");
        }

        private void Log(string eventName)
        {
            Debug.Log($"[frame {Time.frameCount}] {gameObject.name}: {eventName}");
        }
    }
}