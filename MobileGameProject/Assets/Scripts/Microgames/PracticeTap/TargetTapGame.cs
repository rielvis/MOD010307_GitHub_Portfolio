using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    // Week 2 practice microgame: tap the shrinking target quickly to score points.
    public sealed class TargetTapGame : MicrogameBehaviour
    {
        // Section 2: Fields
        [Header("Scene Preferences")]

        [SerializeField] private RectTransform playArea;
        [SerializeField] private RectTransform target;
        [SerializeField] private Image targetImage;
        [SerializeField] private Text progressText;
        [SerializeField] private Text feedbackText;

        [Header("Rules")]

        [Tooltip("Points needed to win before the timer runs out.")]
        [SerializeField, Min(1)] private int scoreToWin = 10;

        [Header("Shrinking target")]

        [SerializeField, Min(10f)] private float startSize = 240f;
        [SerializeField, Min(10f)] private float minimumSize = 100f;

        [Tooltip("How many pixels the target loses from its width and height every second.")]
        [SerializeField, Range(0f, 300f)] private float shrinkPerSecond = 80f;
        [Header("Presentation")]
        [SerializeField] private Color safeColour = new Color(0.20f, 0.80f, 0.40f);
        [SerializeField] private bool showReactionTime = true;
        private int score;
        private float reactionTimer;
        private float currentSize;

        // Section 3: Starting a Run
        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            score = 0;
            targetImage.color = safeColour;
            feedbackText.text = "Go!";
            UpdateProgress();
            MoveTarget();
        }

        private void Update()
        {
            if (!IsRunning) return;

            reactionTimer += Time.deltaTime;
            currentSize -= shrinkPerSecond * Time.deltaTime;
            target.sizeDelta = new Vector2(currentSize, currentSize);

            if (currentSize <= minimumSize)
            {
                feedbackText.text = "Too slow!";
                MoveTarget();
            }        
        }

        // Section 4: Handling a Tap
        public void TapTarget()
        {
            if (!IsRunning) return;

            score = score + 1;
            UpdateProgress();

            if (showReactionTime) 
                feedbackText.text = $"Hit! {reactionTimer:0.00}s";
            else 
                feedbackText.text = "Hit!";

            if (score >= scoreToWin) 
                Win();             
            else 
                MoveTarget();
        }
        
        // Section 5: Helper Methods
        private void UpdateProgress()
        {
            progressText.text = $"Score: {score} / {scoreToWin}";
        }

        private void MoveTarget()
        {
            currentSize = startSize;
            target.sizeDelta = new Vector2(currentSize, currentSize);
            reactionTimer = 0f;

            float maxX = (playArea.rect.width - target.rect.width) * 0.5f;
            float maxY = (playArea.rect.height - target.rect.height) * 0.5f;
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(-maxY, maxY);
            target.anchoredPosition = new Vector2(x, y);
        }
    }
}