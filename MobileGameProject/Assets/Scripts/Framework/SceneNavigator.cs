using UnityEngine;
using UnityEngine.SceneManagement;

namespace MicrogameCourse.Framework
{
    public sealed class SceneNavigator : MonoBehaviour
    {
        private const string MenuScene = "MainMenu";
        private const string PracticeScene = "PracticeTap";
        private const string MG1 = "MG1_DuckDuck";
        private const string MG2 = "MG2_Rubitals";
        private const string MG3 = "MG3_Specifine";
        private const string MG4 = "MG4_USort";

        private bool isLoading;

        public void OpenPractice()
        {
            Load(PracticeScene);
        }

        public void OpenMG1()
        {
            Load(MG1);
        }

        public void OpenMG2()
        {
            Load(MG2);
        }

        public void OpenMG3()
        {
            Load(MG3);
        }

        public void OpenMG4()
        {
            Load(MG4);
        }

        public void OpenMenu()
        {
            Load(MenuScene);
        }

        public void Replay()
        {
            Load(SceneManager.GetActiveScene().name);
        }

        private void Load(string sceneName)
        {
            if (isLoading) return;

            if (!Application.CanStreamedLevelBeLoaded(sceneName))
            {
                Debug.LogError($"Add {sceneName} to File > Build Profiles > Scene List.");
                return;
            }

            isLoading = true;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}