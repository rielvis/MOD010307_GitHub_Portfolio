using UnityEngine;
using UnityEngine.UI; // why do we have to specify this

namespace MicrogameCourse.Framework
{
    public class MicrogameSession : MonoBehaviour
    {
        private enum Phase {Ready, Playing, Result}
        
        [SerializeField] private MicrogameBehaviour game;
        [SerializeField] private GameObject readyPanel;
        [SerializeField] private GameObject playArea;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private Text resultText;
        
        

        private Phase currentPhase;
        

        public virtual void Awake()
        {
            currentPhase = Phase.Ready;
            readyPanel.SetActive(true);
            playArea.SetActive(false);
            resultPanel.SetActive(false);

        }

        public virtual void StartGame()
        {
            if (currentPhase != Phase.Ready || game == null) return;

            
            readyPanel.SetActive(false);
            playArea.SetActive(true);
            resultPanel.SetActive(false);
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
            resultPanel.SetActive(true);
            resultText.text = won ? "You win!" : "Game over!"; // Ternery statement
        }

    }
}
