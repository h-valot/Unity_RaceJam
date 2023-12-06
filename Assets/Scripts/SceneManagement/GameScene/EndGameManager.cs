using Data;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public ScoreManager scoreManager;

    private bool _doHandleEnd;
    
    public void Initialize()
    {
        Events.onCircuitEnded += HandleEnd;
    }

    private void OnDisable()
    {
        Events.onCircuitEnded -= HandleEnd;
    }

    /// <summary>
    /// Manages end game and load a new game or menu scene
    /// </summary>
    private async void HandleEnd(EndSituation endSituation)
    {
        // cannot enter this fonction several times
        if (_doHandleEnd) return;
        _doHandleEnd = true;

        scoreManager.HandleEnd(endSituation);

        await scoreManager.scoreUIManager.AnimateEndCircuit(endSituation, scoreManager.GetTime());
        
        // scene management
        DataManager.data.raceAmount++;
        if (DataManager.data.raceAmount < Registry.gameConfig.raceAmount)
        {
            // reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // save highest score
            if (DataManager.data.score > DataManager.data.highestScore)
                DataManager.data.highestScore = DataManager.data.score;
            DataManager.Save();
            
            await scoreManager.scoreUIManager.AnimateEndCycle();
            
            // load main menu scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}