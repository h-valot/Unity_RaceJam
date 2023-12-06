using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InitLoadingScreenUIManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public TextMeshProUGUI gameNameTM;
    public TextMeshProUGUI companyNameTM;

    [Header("GLOBAL SETTINGS")] 
    public float timeBetweenAnimations;
    
    [Header("NAME SETTINGS")] 
    public float namesApparitionDuration;
    public float namesScaleUpDuration;
    public float namesDisparitionDuration;
    
    public async Task AnimateLoadingScreen()
    {
        await AnimateName(gameNameTM);
        await AnimateName(companyNameTM);
    }

    private async Task AnimateName(TextMeshProUGUI nameTM)
    {
        nameTM.DOFade(1, namesApparitionDuration);
        await Task.Delay(Mathf.RoundToInt(1000 * namesApparitionDuration));
        
        nameTM.transform.DOScale(1.3f, namesScaleUpDuration);
        await Task.Delay(Mathf.RoundToInt(1000 * namesScaleUpDuration));
        
        nameTM.DOFade(0, namesDisparitionDuration);
        await Task.Delay(Mathf.RoundToInt(1000 * namesDisparitionDuration));
        
        // wait the inbetween animation time
        await Task.Delay(Mathf.RoundToInt(1000 * timeBetweenAnimations));
    }
}