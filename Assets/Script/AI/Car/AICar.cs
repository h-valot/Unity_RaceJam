using System;
using Script.AI.StimulusSources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Script.AI.Car
{
    public class AICar : MonoBehaviour
    {
        [SerializeField]
        private bool focusPlayer = false;

        [SerializeField] 
        private Rigidbody rigidBody;

        [SerializeField] 
        private float speed = 1.0f;

        private Vector3 _direction;

        private void Start()
        {
            if (rigidBody == null)
            {
                rigidBody = this.GetComponent<Rigidbody>();
            }
        }
        
        public void OnSightWall(AIStimulusResult aiStimulusResult)
        {
            foreach (var other in aiStimulusResult.otherTansforms)
            {
                this._direction += (this.transform.position - other.position).normalized;
            }

            this._direction.Normalize();
        }
        
        public void OnSightPlayer(AIStimulusResult aiStimulusResult)
        {
            if (this.focusPlayer)
            {
                foreach (var other in aiStimulusResult.otherTansforms)
                {
                    this._direction += (other.position - this.transform.position).normalized;
                }

                this._direction.Normalize();
            }
        }

        private void FixedUpdate()
        {
            this.rigidBody.AddRelativeForce(this._direction * speed, ForceMode.Acceleration);
        }
    }
}