using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HighestScoreUIDisplay : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public TextMeshProUGUI highestScoreTM;

        public void UpdateDisplay()
        {
            highestScoreTM.text = $"Highest score: {DataManager.data.highestScore}";
        }
    }
}