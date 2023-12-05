using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public HighestScoreUIDisplay highestScoreUIDisplay;
        public GameObject settingsParent, creditsParent;
    
        public void Initialize()
        {
            highestScoreUIDisplay.UpdateDisplay();
            CloseCredits();
            CloseSettings();
        }

        public void Play()
        {
            DataManager.data.score = 0;
            DataManager.data.raceAmount = 0;
            SceneManager.LoadScene("Game");
        }

        public void LoadGarage()
        {
            SceneManager.LoadScene("Garage");
        }

        public void OpenSettings() => settingsParent.SetActive(true);
        public void CloseSettings() => settingsParent.SetActive(false);
        public void OpenCredits() => creditsParent.SetActive(true);
        public void CloseCredits() => creditsParent.SetActive(false);
    }
}