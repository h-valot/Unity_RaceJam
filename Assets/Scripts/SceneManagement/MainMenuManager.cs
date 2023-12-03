using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public HighestScoreDisplay highestScoreDisplay;
    public GameObject settingsParent, creditsParent;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }
        
        highestScoreDisplay.UpdateDisplay();
        CloseCredits();
        CloseSettings();
    }

    public void Play()
    {
        DataManager.data.score = 0;
        DataManager.data.raceAmount = 0;
        SceneManager.LoadScene("Map");
    }

    public void OpenSettings() => settingsParent.SetActive(true);
    public void CloseSettings() => settingsParent.SetActive(false);
    public void OpenCredits() => creditsParent.SetActive(true);
    public void CloseCredits() => creditsParent.SetActive(false);
}
