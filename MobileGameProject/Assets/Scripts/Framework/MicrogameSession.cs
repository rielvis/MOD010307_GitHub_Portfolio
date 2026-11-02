using UnityEngine;
using UnityEngine.UI; // why do we have to specify this

namespace MicrogameCourse.Framework
{
    public class MicrogameSession : MonoBehaviour
    {
        private enum Phase {Ready, Playing, Result}
        
        [SerializeField] private MicrogameBehaviour game; // the "game manager"
        [SerializeField] private GameObject readyPanel; // UI (Canvas) > Image
        [SerializeField] private GameObject playArea;
        [SerializeField] private GameObject playUI;
        [SerializeField] private GameObject resultPanel; // UI (Canvas) > Image
        [SerializeField] private GameObject player;
        [SerializeField] private Text resultText; // TMP
        [SerializeField, Min(1f)] private float durationSeconds = 10f;

        private Phase currentPhase;
        

        public virtual void Awake()
        {
            currentPhase = Phase.Ready;
            readyPanel.SetActive(true);
            playArea.SetActive(false);
            playUI.SetActive(false);
            resultPanel.SetActive(false);
            player.SetActive(false);
        }

        public virtual void StartGame()
        {
            if (currentPhase != Phase.Ready || game == null) return;
            readyPanel.SetActive(false);
            playArea.SetActive(true);
            playUI.SetActive(true);
            resultPanel.SetActive(false);
            player.SetActive(true);
            currentPhase = Phase.Playing;
            game.Begin(this);
        }

        public virtual void Update()
        {
            if (currentPhase != Phase.Playing) return;
        }

        public virtual void Finish(bool won)
        {
            if (currentPhase != Phase.Playing) return;

            currentPhase = Phase.Result;
            game.End();
            playArea.SetActive(false);
            playUI.SetActive(false);
            resultPanel.SetActive(true);
            player.SetActive(false);
            resultText.text = won ? "You win!" : "Time is up!"; // Ternery statement
        }

    }
}
