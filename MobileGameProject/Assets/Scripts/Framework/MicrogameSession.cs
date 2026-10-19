using UnityEngine;
using UnityEngine.UI; // why do we have to specify this

namespace MicrogameCourse.Framework
{
    public sealed class MicrogameSession : MonoBehaviour
    {
        private enum Phase {Ready, Playing, Result}
        
        [SerializeField] private MicrogameBehaviour game; // the "game manager"
        [SerializeField] private GameObject readyPanel; // UI (Canvas) > Image
        [SerializeField] private GameObject playArea;
        [SerializeField] private GameObject resultPanel; // UI (Canvas) > Image
        [SerializeField] private Text timerText; // TMP
        [SerializeField] private Text resultText; // TMP
        [SerializeField, Min(1f)] private float durationSeconds = 10f;

        private Phase currentPhase;
        private float remainingSeconds;

        private void Awake()
        {
            currentPhase = Phase.Ready;
            readyPanel.SetActive(true);
            playArea.SetActive(false);
            resultPanel.SetActive(false);
            timerText.text = string.Empty;
        }

        public void StartGame()
        {
            if (currentPhase != Phase.Ready || game == null) return;

            remainingSeconds = durationSeconds;
            readyPanel.SetActive(false);
            playArea.SetActive(true);
            resultPanel.SetActive(false);
            currentPhase = Phase.Playing;
            ShowTime();
            game.Begin(this);
        }

        private void Update()
        {
            if (currentPhase != Phase.Playing) return;

            remainingSeconds = Mathf.Max(0f, remainingSeconds - Time.deltaTime);
            ShowTime();
            if (remainingSeconds <= 0) Finish(false);
        }

        public void Finish(bool won)
        {
            if (currentPhase != Phase.Playing) return;

            currentPhase = Phase.Result;
            game.End();
            playArea.SetActive(false);
            resultPanel.SetActive(true);
            resultText.text = won ? "You win!" : "Time is up!"; // Ternery statement
        }

        private void ShowTime()
        {
            timerText.text = $"Time : {remainingSeconds:0.0}s";
        }
    }
}
