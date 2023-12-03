using Data;
using TMPro;
using UnityEngine;

public class HighestScoreDisplay : MonoBehaviour
{
    [Header("REFERENCES")] 
    public TextMeshProUGUI highestScoreTM;

    public void UpdateDisplay()
    {
        highestScoreTM.text = $"Highest score: {DataManager.data.highestScore}";
    }
}