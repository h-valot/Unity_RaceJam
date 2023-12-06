using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGameUIManager : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public GameObject graphicsParent;
        public Image circleAnimationImage;
        public TextMeshProUGUI counterTM;

        public void Initialize()
        {
            graphicsParent.transform.DOScale(Vector3.one, 0).SetUpdate(true);
        }

        public async Task AnimateStart()
        {
            circleAnimationImage.DOFillAmount(1, Registry.gameConfig.startCircuitNumberDuration * Registry.gameConfig.startCircuitText.Length).SetUpdate(true);

            foreach (string text in Registry.gameConfig.startCircuitText)
            {
                counterTM.text = text;
                await Task.Delay(Mathf.RoundToInt(1000 * Registry.gameConfig.startCircuitNumberDuration));
            }
            
            graphicsParent.transform.DOScale(Vector3.zero, 0).SetUpdate(true);
            circleAnimationImage.transform.DOScale(Vector3.zero, 0).SetUpdate(true);
            counterTM.transform.DOScale(Vector3.zero, 0).SetUpdate(true);
        }
    }
}