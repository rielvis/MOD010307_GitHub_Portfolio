using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace MicrogameCourse.Microgames
{
    public sealed class TargetTapGame : MicrogameBehaviour
    {
        // Section 2: Fields
        [SerializeField] private RectTransform playArea;
        [SerializeField] private RectTransform target;
        [SerializeField] private Text progressText;
        [SerializeField, Min(1)] private int tapsToWin = 5;
        [SerializeField] private float startSize = 240f;
        [SerializeField] private float minimumSize = 100f;
        [SerializeField] private float shrinkPerSecond = 80f;

        private int tapsRemaining;
        private float currentSize;

        // Section 3: Starting a Run
        public override void Begin(MicrogameSession session)
        {
            base.Begin(session);
            tapsRemaining = tapsToWin;
            UpdateProgress();
            MoveTarget();
        }

        private void Update()
        {
            if (!IsRunning) return;
            currentSize -= shrinkPerSecond * Time.deltaTime;
            target.sizeDelta = new Vector2(currentSize, currentSize);
            if (currentSize <= minimumSize)
                MoveTarget();        
        }

        // Section 4: Handling a Tap
        public void TapTarget()
        {
            if (!IsRunning) return;

            tapsRemaining--;
            UpdateProgress();

            if (tapsRemaining == 0)
            {
                Win();
            }
            else
            {
                MoveTarget();
            }
        }
        
        // Section 5: Helper Methods
        private void UpdateProgress()
        {
            progressText.text = $"Taps left: {tapsRemaining}";
        }

        private void MoveTarget()
        {
            currentSize = startSize;
            target.sizeDelta = new Vector2(currentSize, currentSize);
            
            float maxX = (playArea.rect.width - target.rect.width) * 0.5f;
            float maxY = (playArea.rect.height - target.rect.height) * 0.5f;
            float x = Random.Range(-maxX, maxX);
            float y = Random.Range(-maxY, maxY);
            target.anchoredPosition = new Vector2(x, y);
        }
    }
}