using System.Threading.Tasks;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
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

    public void HandleEnd(bool wallHitten)
    {
        StopTimer();
        if (wallHitten) LoseScore();
        else AddScore();
    }
    
    public void AddScore()
    {
        DataManager.data.score += Mathf.CeilToInt(currentTime);
        if (DataManager.data.score < 0) DataManager.data.score = 0;
        DisplayScore();
    }

    public void LoseScore()
    {
        DataManager.data.score -= Registry.gameConfig.scoreLossPlayerHitWall;
        if (DataManager.data.score < 0) DataManager.data.score = 0;
        DisplayScore();
    }
    
    public async Task AnimateEndCircuit(bool wallHitten)
    {
        endCircuitParent.SetActive(true);

        endCircuitTitleTM.text = wallHitten ? "FAILED" : "COMPLETED";
        endCircuitTimeTM.text = wallHitten ? "Score time: 0" : $"Score time: {Mathf.CeilToInt(currentTime)}";
        endCircuitTotalScoreTM.text = $"Total score: {DataManager.data.score}";
        
        endCircuitSentenceTM.text = wallHitten
            ? Registry.gameConfig.loserSentences[Random.Range(0, Registry.gameConfig.loserSentences.Length)]
            : Registry.gameConfig.winnerSentences[Random.Range(0, Registry.gameConfig.winnerSentences.Length)];

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
    public void StopTimer() => _canRunTimer = false;
}