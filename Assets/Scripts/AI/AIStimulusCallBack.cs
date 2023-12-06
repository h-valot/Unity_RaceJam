using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class AIStimulusCallBack : MonoBehaviour
    {
        public AIStimulus aiStimulus;
        
        public UnityEvent<AIStimulusResult> onReceiveStimulus;
        public UnityEvent onSight;

        public void CallBack(AIStimulus stimulus, AIStimulusResult stimulusResult)
        {
            foreach (var tag in aiStimulus.tags) {
                if (stimulus.HaveTag(tag))
                {
                    this.onReceiveStimulus?.Invoke(stimulusResult);
                    this.onSight?.Invoke();
                    return;
                }
            }
        }
    }
}