using System.Threading.Tasks;
using DG.Tweening;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public HighestScoreUIDisplay highestScoreUIDisplay;
        public GameObject settingsParent, creditsParent;
        public Image blackFadeImage;

        [Header("SETTINGS")] 
        public float introFadeDuration;
    
        public async void Initialize()
        {
            highestScoreUIDisplay.UpdateDisplay();
            CloseCredits();
            CloseSettings();
            await AnimateMenuEntrance();
        }

        private async Task AnimateMenuEntrance()
        {
            blackFadeImage.DOFade(0, introFadeDuration);
            await Task.Delay(Mathf.RoundToInt(1000 * introFadeDuration));
            blackFadeImage.gameObject.SetActive(false);
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