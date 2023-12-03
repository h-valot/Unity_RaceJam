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

        [SerializeField] 
        private Transform target;

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
                    this._direction += other.position - this.transform.position;
                }
            }
        }

        public void OnSightOtherEnemyCar(AIStimulusResult aiStimulusResult)
        {
            this.OnSightWall(aiStimulusResult);
        }

        private void FixedUpdate()
        {
            this._direction += (target.position - this.transform.position).normalized;
            
            this._direction.y = 0.0f;
            this._direction.Normalize();
            this.rigidBody.velocity = this._direction * speed;
            this._direction = Vector3.zero;
        }
    }
}