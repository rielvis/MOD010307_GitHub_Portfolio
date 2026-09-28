using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    /// <summary>
    /// Week 2 practice microgame (Target Tap+): tap green targets quickly for
    /// more points, ignore red decoys and reach the score before time runs out.
    /// </summary>    
    public sealed class TargetTapGame : MicrogameBehaviour
    {
        [Header("Scene references")]
        [SerializeField] private RectTransform playArea;
        [SerializeField] private RectTransform target;
        [SerializeField] private Image targetImage;
        [SerializeField] private Text progressText;
        [SerializeField] private Text feedbackText;

        [Header("Rules")]
        [Tooltip("Points needed to win before the timer runs out.")]
        [SerializeField, Min(1)] private int scoreToWin = 10;
        [Tooltip("Chance (0 = never, 1 = always) that the next target is a decoy.")]
        [SerializeField, Range(0f, 1f)] private float decoyChance = 0.25f;
        [Tooltip("Points lost for tapping a decoy.")]
        [SerializeField, Min(0)] private int decoyPenalty = 2;
        [Header("Reaction scoring (seconds)")]
        [SerializeField, Min(0f)] private float perfectTime = 0.5f;
        [SerializeField, Min(0f)] private float greatTime = 1.0f;
        [
Header("Shrinking target")]
        [SerializeField, Min(10f)] private float startSize = 240f;
        [SerializeField, Min(10f)] private float minimumSize = 100f;
        [Tooltip("How many pixels the target loses from its width and height every second.")]
        [SerializeField, Range(0f, 300f)] private float shrinkPerSecond = 80f;

        [Header("Presentation")]
        [SerializeField] private Color safeColour = new Color(0.20f, 0.80f, 0.40f);
        [SerializeField] private Color decoyColour = new Color(0.90f, 0.20f, 0.20f); [
SerializeField]
        private bool showReactionTime = true;

        private int score;
        private float reactionTimer;
        private float currentSize;
        private bool isDecoy;

        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            score = 0;
            isDecoy = false;
            ShowFeedback("Go!");
            UpdateProgress();
            ShowNextTarget();
        }

        private void Update()
        {
            if (!IsRunning) return;

            reactionTimer += Time.deltaTime;
            SetTargetSize(currentSize - shrinkPerSecond * Time.deltaTime);

            if (IsTargetTooSmall())
                TargetExpired();
        }


        public void TapTarget()
        {
            if (!IsRunning) return;

            if (isDecoy)
            {
                AddScore(-decoyPenalty);
                ShowFeedback($"Decoy! -{decoyPenalty}");
                ShowNextTarget();
                return;
            }
            int points = CalculatePoints(reactionTimer);
            AddScore(points);
            string message = $"{GetRating(points)} +{points}";
            if (showReactionTime)
                message += $"  ({reactionTimer:0.00}s)";
            ShowFeedback(message);

            if (score >= scoreToWin)
                Win();
            else
                ShowNextTarget();
        }

        private void TargetExpired()
        {
            if (isDecoy)
                ShowFeedback("Good dodge!");
            else
                ShowFeedback("Too slow!");
            ShowNextTarget();
        }

        private void ShowNextTarget()
        {
            // No decoy while the score is zero, and never two decoys in a row.
            bool wasDecoy = isDecoy;
            if (score == 0 || wasDecoy)
                isDecoy = false;
            else
                isDecoy = Random.value < decoyChance;
            if (isDecoy)
                SetTargetColour(decoyColour);
            else
                SetTargetColour(safeColour);

            SetTargetSize(startSize);
            target.anchoredPosition = GetRandomPosition(playArea, startSize);
            reactionTimer = 0f;
        }

        private int CalculatePoints(float reactionTime)
        {
            if (reactionTime <= perfectTime)
                return 3;
            else if (reactionTime <= greatTime)
                return 2;
            else
                return 1;
        }
        private string GetRating(int points)
        {
            if (points == 3)
                return "Perfect!";
            else if (points == 2)
                return "Great!";
            else
                return "Good";
        }

        private void AddScore(int amount)
        {
            score += amount;
            if (score < 0)
                score = 0;
            UpdateProgress();
        }

        private void SetTargetSize(float size)
        {
            currentSize = size;
            target.sizeDelta = new Vector2(size, size);
        }

        private void SetTargetColour(Color colour)
        {
            targetImage.color = colour;
        }

        private void ShowFeedback(string message)
        {
            feedbackText.text = message;
        }

        private void UpdateProgress()
        {
            progressText.text = $"Score: {score} / {scoreToWin}";
        }

        private bool IsTargetTooSmall()
        {
            return currentSize <= minimumSize;
        }

        private Vector2 GetRandomPosition(RectTransform area, float itemSize)
        {
            float maxX = (area.rect.width - itemSize) * 0.5f;
            float maxY = (area.rect.height - itemSize) * 0.5f;
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(-maxY, maxY);
            return new Vector2(x, y);
        }
    }
}