using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // why do we have to specify this

namespace MicrogameCourse.Framework
{
    public class MicrogameSession : MonoBehaviour
    {
        private enum Phase {Ready, Playing, Paused, Result}
        
        [SerializeField] private MicrogameBehaviour game; // the "game manager"
        [SerializeField] private GameObject readyPanel; // UI (Canvas) > Image
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject playArea;
        [SerializeField] private GameObject playUI;
        [SerializeField] private GameObject resultPanel; // UI (Canvas) > Image
        [SerializeField] private GameObject player;
        [SerializeField] private Text resultText; // TMP

        private Phase currentPhase;
        

        public virtual void Awake()
        {
            currentPhase = Phase.Ready;
            readyPanel.SetActive(true);
            pausePanel.SetActive(false);
            pauseButton.SetActive(false);
            playArea.SetActive(false);
            playUI.SetActive(false);
            resultPanel.SetActive(false);
            player.SetActive(false);
        }

        public virtual void StartGame()
        {
            if (currentPhase != Phase.Ready || game == null) return;

            currentPhase = Phase.Playing;

            readyPanel.SetActive(false);
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
            playArea.SetActive(true);
            playUI.SetActive(true);
            resultPanel.SetActive(false);
            player.SetActive(true);
            
            game.Begin(this);
        }

        public virtual void PauseGame()
        {
            currentPhase = Phase.Paused;

            readyPanel.SetActive(false);
            pausePanel.SetActive(true);
            pauseButton.SetActive(true);
            playArea.SetActive(true);
            playUI.SetActive(true);
            resultPanel.SetActive(false);
            player.SetActive(true);

            game.IsRunning = false;
        }

        public virtual void ResumeGame()
        {
            currentPhase = Phase.Playing;

            readyPanel.SetActive(false);
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
            playArea.SetActive(true);
            playUI.SetActive(true);
            resultPanel.SetActive(false);
            player.SetActive(true);
            
            game.IsRunning = true;
        }

        public virtual void Update()
        {
            if (currentPhase != Phase.Playing) return;
        }

        public virtual void Finish(bool won)
        {
            if (currentPhase != Phase.Playing) return;

            currentPhase = Phase.Result;

            readyPanel.SetActive(false);
            pausePanel.SetActive(false);
            pauseButton.SetActive(false);
            playArea.SetActive(false);
            playUI.SetActive(false);
            resultPanel.SetActive(true);
            player.SetActive(false);

            game.End();
            resultText.text = won ? "You win!" : "Game over!"; // Ternery statement
        }

    }
}
