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
            graphicsParent.SetActive(true);
        }

        public async void AnimateStart()
        {
            circleAnimationImage.DOFillAmount(1, Registry.gameConfig.startCircuitNumberDuration * Registry.gameConfig.startCircuitText.Length).SetUpdate(true);

            foreach (string text in Registry.gameConfig.startCircuitText)
            {
                counterTM.text = text;
                await Task.Delay(Mathf.RoundToInt(1000 * Registry.gameConfig.startCircuitNumberDuration));
            }
            
            graphicsParent.SetActive(false);
            circleAnimationImage.gameObject.SetActive(false);
            counterTM.gameObject.SetActive(false);
        }
    }
}