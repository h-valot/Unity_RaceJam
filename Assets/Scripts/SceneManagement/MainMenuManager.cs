using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public HighestScoreDisplay highestScoreDisplay;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }
        
        highestScoreDisplay.UpdateDisplay();
    }

    public void Play()
    {
        DataManager.data.score = 0;
        DataManager.data.raceAmount = 0;
        SceneManager.LoadScene("Map");
    }
}
