using UnityEngine;

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

        public void Initialize(Transform finishCell)
        {
            this.target = finishCell;
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

        private void LookAtNextPosition()
        {
            this.transform.LookAt(this.rigidBody.velocity);
            var eulerAngles = this.transform.rotation.eulerAngles;
            eulerAngles.x = 0.0f;
            eulerAngles.z = 0.0f;
        }

        private void FixedUpdate()
        {
            this._direction += (target.position - this.transform.position).normalized / 1.5f;
            
            this._direction.y = 0.0f;
            this._direction.Normalize();
            this.rigidBody.velocity = this._direction * speed;

            this.LookAtNextPosition();

            this._direction = Vector3.zero;
        }
    }
}