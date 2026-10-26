using UnityEngine;
using UnityEngine.UI;
using MicrogameCourse.Framework;

namespace MicrogameCourse.Microgames
{
    public class TargetTapSession : MicrogameSession
    {
        [SerializeField, Min(1f)] private float durationSeconds = 10f;        
        [SerializeField] private Text timerText;

        private float remainingSeconds;

        public override void Awake()
        {
            base.Awake();

            timerText.text = string.Empty;
        }

        public override void StartGame()
        {
            base.StartGame();

            remainingSeconds = durationSeconds;
            ShowTime();       
        }

        public override void Update()
        {
            base.Update();

            remainingSeconds = Mathf.Max(0f, remainingSeconds - Time.deltaTime);
            ShowTime();
            if (remainingSeconds <= 0) Finish(false);
        }

        private void ShowTime()
        {
            timerText.text = $"Time : {remainingSeconds:0.0}s";
        }
    }
}