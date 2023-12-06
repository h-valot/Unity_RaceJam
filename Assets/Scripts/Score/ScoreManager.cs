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

        /// <summary>
        /// Sets the timer start value based on the circuit size and the score multiplier
        /// </summary>
        public void Initialize()
        {
            _currentTime = Registry.mapConfig.circuitSize * Registry.gameConfig.scoreMultiplier;
            scoreUIManager.Initialize();
        }
        
        private void Update()
        {
            if (!_canRunTimer) return;
        
            _currentTime -= Time.deltaTime;
            scoreUIManager.UpdateTimeDisplay(_currentTime);
        }
        
        /// <summary>
        /// Stops the timer and attributes score based on the end situation
        /// </summary>
        /// <param name="endSituation">PLAYER_WINS add score based on timer - OTHERS lose fix amount of score</param>
        public void HandleEnd(EndSituation endSituation)
        {
            StopTimer();
            if (endSituation == EndSituation.PLAYER_WINS) AddScore();
            else LoseScore();
        }
    
        /// <summary>
        /// Add score based on the timer
        /// </summary>
        private void AddScore()
        {
            DataManager.data.score += Mathf.CeilToInt(_currentTime);
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            scoreUIManager.UpdateScoreDisplay();
        }

        /// <summary>
        /// Lose score based on a fixed value setted in game config
        /// </summary>
        private void LoseScore()
        {
            DataManager.data.score -= Registry.gameConfig.loseScoreAmount;
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            scoreUIManager.UpdateScoreDisplay();
        }

        private void StopTimer() => _canRunTimer = false;
        public void StartTimer() => _canRunTimer = true;
        public float GetTime() => _currentTime;
    }
}