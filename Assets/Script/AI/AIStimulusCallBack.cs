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
            if (stimulus.name == aiStimulus.name)
            {
                onReceiveStimulus?.Invoke(stimulusResult);
            }
        }
    }
}