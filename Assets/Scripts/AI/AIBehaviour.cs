using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIBehaviour : MonoBehaviour
    {
        [SerializeField] private List<AIStimulusCallBack> aiStimulusCallBacks;

        public void UpdateAI(AIStimulus aiStimulus, AIStimulusResult aiStimulusResult)
        {
            foreach (var callBack in aiStimulusCallBacks)
            {
                callBack.CallBack(aiStimulus, aiStimulusResult);
            }
        }
    }
}