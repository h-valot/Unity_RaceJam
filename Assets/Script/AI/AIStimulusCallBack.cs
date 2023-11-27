using UnityEngine;
using UnityEngine.Events;

namespace Script.AI
{
    public class AIStimulusCallBack : MonoBehaviour
    {
        public AIStimulus aiStimulus;
        
        public UnityEvent<AIStimulusResult> onReceiveStimulus;

        public void CallBack(AIStimulus stimulus, AIStimulusResult stimulusResult)
        {
            foreach (var tag in aiStimulus.tags) {
                if (stimulus.HaveTag(tag))
                {
                    onReceiveStimulus?.Invoke(stimulusResult);
                    return;
                }
            }
        }
    }
}