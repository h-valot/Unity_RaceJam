using System;
using System.Threading.Tasks;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class ScoreUIManager : MonoBehaviour
    {
        [Header("HUD")]
        public TextMeshProUGUI scoreTM;
        public TextMeshProUGUI timerTM;

        [Header("END CIRCUIT")] 
        public GameObject endCircuitParent;
        public Image endCircuitFillImage;
        public TextMeshProUGUI endCircuitTitleTM;
        public TextMeshProUGUI endCircuitSentenceTM;
        public TextMeshProUGUI endCircuitTimeTM;
        public TextMeshProUGUI endCircuitTotalScoreTM;
    
        [Header("END CIRCUIT")] 
        public GameObject endCycleParent;
        public Image endCycleFillImage;
        public TextMeshProUGUI endCycleTotalScoreTM;
        public TextMeshProUGUI endCycleHighestScoreTM;
    
        [HideInInspector] public float currentTime;
        private bool _canRunTimer;

        public void Initialize()
        {
            currentTime = Registry.mapConfig.circuitSize * Registry.gameConfig.scoreMultiplier;
        
            DisplayScore();
            StartTimer();
        }
    
        private void Update()
        {
            if (!_canRunTimer) return;
        
            currentTime -= Time.deltaTime;
            timerTM.text = $"Time: {Mathf.CeilToInt(currentTime)}s";
        }

        public void HandleEnd(EndSituation endSituation)
        {
            StopTimer();
            if (endSituation == EndSituation.PLAYER_WINS) AddScore();
            else LoseScore();
        }
    
        private void AddScore()
        {
            DataManager.data.score += Mathf.CeilToInt(currentTime);
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            DisplayScore();
        }

        private void LoseScore()
        {
            DataManager.data.score -= Registry.gameConfig.loseScoreAmount;
            if (DataManager.data.score < 0) DataManager.data.score = 0;
            DisplayScore();
        }
    
        public async Task AnimateEndCircuit(EndSituation endSituation)
        {
            endCircuitParent.SetActive(true);

            switch (endSituation)
            {
                case EndSituation.PLAYER_WINS:
                    endCircuitTitleTM.text = "COMPLETED";
                    endCircuitTimeTM.text = $"Score: {Mathf.CeilToInt(currentTime)}";
                    endCircuitSentenceTM.text = Registry.gameConfig.playerWinsSentences[Random.Range(0, Registry.gameConfig.playerWinsSentences.Length)];
                    break;
                case EndSituation.AI_WINS:
                    endCircuitTitleTM.text = "YOU GOT OVERTAKEN";
                    endCircuitTimeTM.text = $"Score: {-Registry.gameConfig.loseScoreAmount}";
                    endCircuitSentenceTM.text = Registry.gameConfig.aiWinsSentences[Random.Range(0, Registry.gameConfig.aiWinsSentences.Length)];
                    break;
                case EndSituation.PLAYER_DEAD:
                    endCircuitTitleTM.text = "FAILED";
                    endCircuitTimeTM.text = $"Score: {-Registry.gameConfig.loseScoreAmount}";
                    endCircuitSentenceTM.text = Registry.gameConfig.playerDeadSentences[Random.Range(0, Registry.gameConfig.playerDeadSentences.Length)];
                    break;
            }
            endCircuitTotalScoreTM.text = $"Total score: {DataManager.data.score}";
        
            var sequence = DOTween.Sequence();
            sequence.SetLoops(-1);
            sequence.Append(endCircuitSentenceTM.transform.DOScale(1.3f, 0.5f));
            sequence.Append(endCircuitSentenceTM.transform.DOScale(1f, 0.5f));
        
            endCircuitFillImage.DOFillAmount(1, Registry.gameConfig.endCircuitScoreDuration);
        
            await Task.Delay(Mathf.RoundToInt(1000 * Registry.gameConfig.endCircuitScoreDuration));
            if (endCircuitParent != null) endCircuitParent.SetActive(false);
        }

        public async Task AnimateEndCycle()
        {
            endCycleParent.SetActive(true);
        
            endCycleTotalScoreTM.text = $"Total score: {DataManager.data.score}";
            endCycleHighestScoreTM.text = $"Highest score: {DataManager.data.highestScore}";
            endCycleFillImage.DOFillAmount(1, Registry.gameConfig.endCycleScoreDuration);
        
            await Task.Delay(1000 * Mathf.RoundToInt(Registry.gameConfig.endCycleScoreDuration));
            if (endCycleParent != null) endCycleParent.SetActive(false);
        }
    
        private void DisplayScore() => scoreTM.text = $"Score: {DataManager.data.score}";
        private void StartTimer() => _canRunTimer = true;
        private void StopTimer() => _canRunTimer = false;
    }
}