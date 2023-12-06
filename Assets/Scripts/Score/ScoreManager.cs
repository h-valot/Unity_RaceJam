using Data;
using UI;
using UnityEngine;

namespace Score
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("EXTERNAL REFERENCES")]
        public ScoreUIManager scoreUIManager;
        
        private float _currentTime;
        private bool _canRunTimer;

        public void Initialize()
        {
            _currentTime = Registry.mapConfig.circuitSize * Registry.gameConfig.scoreMultiplier;
            scoreUIManager.Initialize();
            StartTimer();
        }
        
        private void Update()
        {
            if (!_canRunTimer) return;
        
            _currentTime -= Time.deltaTime;
            scoreUIManager.UpdateTimeDisplay(_currentTime);
        }
        
        public void HandleEnd(EndSituation endSituation)
        {
            StopTimer();
            if (endSituation == EndSituation.PLAYER_WINS) AddScore();
            else LoseScore();
        }
    
        private void AddScore()
        {
            DataManager.data.score += Mathf.CeilToInt(_currentTime);
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            scoreUIManager.UpdateScoreDisplay();
        }

        private void LoseScore()
        {
            DataManager.data.score -= Registry.gameConfig.loseScoreAmount;
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            scoreUIManager.UpdateScoreDisplay();
        }

        private void StopTimer() => _canRunTimer = false;
        private void StartTimer() => _canRunTimer = true;
        public float GetTime() => _currentTime;
    }
}