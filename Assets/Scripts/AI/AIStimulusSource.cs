using UnityEngine;

namespace AI
{
    public class AIStimulusSource : MonoBehaviour
    {
        [SerializeField]
        private AIStimulus aiStimulus;

        [SerializeField] 
        private AIBehaviour aiBehaviour;

        /// <summary>
        /// Send a stimulus to the AIBehaviour to calls the CallBacks related
        /// </summary>
        /// <param name="stimulusResult"></param>
        public void EmmitStimulus(AIStimulusResult stimulusResult)
        {
            aiBehaviour.UpdateAI(aiStimulus, stimulusResult);
        }
    }
}