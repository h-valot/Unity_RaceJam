using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.AI.StimulusSources
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SightStimulusSource : MonoBehaviour
    {
        [SerializeField] private AIStimulusSource stimulusSource;
        
        [SerializeField] 
        [Tooltip("Detection based on the searched tags.")]
        private List<string> searchedTags;
        
        private Collider _selfCollider;

        [Header("Sight Settings")] 
        [SerializeField]
        private List<string> transparentTags;

        [SerializeField] 
        [Tooltip("Use self transform by default.")]
        private Transform bodyTransform;

        private AIStimulusResult _outputAIStimulusResult = new AIStimulusResult(); 

        private void Start()
        {
            _selfCollider = this.GetComponent<Collider>();
            if (bodyTransform == null)
            {
                bodyTransform = this.transform;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = (other.transform.position - this.transform.position).normalized;
            
            var hits = Physics.RaycastAll(ray);

            if (hits.Length > 0) {
                var arrayStartIndex = 0;
                if (hits.First().collider == this._selfCollider)
                {
                    arrayStartIndex++;
                }

                for (var hit = arrayStartIndex; hit < hits.Length; hit++)
                {
                    var hitIsTransparent = false;
                    foreach (var transparentTag in this.transparentTags)
                    {
                        if (hits[hit].transform.CompareTag(transparentTag))
                        {
                            hitIsTransparent = true;
                            break;
                        }
                    }

                    if (!hitIsTransparent)
                    {
                        this.OnSight(hits[hit].transform);

                        // Call on sight only for the first element
                        return;
                    }
                }
            }
        }

        private void OnSight(Transform other)
        {
            _outputAIStimulusResult.sourceTransform = this.bodyTransform;
            _outputAIStimulusResult.otherTansforms.Add(other);
        }
    }
}