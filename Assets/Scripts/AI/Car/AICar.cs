using UnityEngine;

namespace Script.AI.Car
{
    public class AICar : MonoBehaviour
    {
        [SerializeField] private bool focusPlayer = false;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Transform target;

        private Vector3 _direction;
        private float _speed;

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
                this._direction -= other.position - this.transform.position;
            }

            this._direction = this._direction.normalized;
        }
        
        public void OnSightPlayer(AIStimulusResult aiStimulusResult)
        {
            if (this.focusPlayer)
            {
                foreach (var other in aiStimulusResult.otherTansforms)
                {
                    if (!this.focusPlayer)
                    {
                        this._direction += other.position - this.transform.position;
                    }
                    else
                    {
                        this._direction -= other.position - this.transform.position;
                    }
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
            this.transform.rotation = Quaternion.Euler(eulerAngles);
        }

        private void FixedUpdate()
        {
            this._direction += (target.position - this.transform.position).normalized;
            
            this._direction.y = 0.0f;
            this.rigidBody.velocity = this._direction * _speed;

            this.LookAtNextPosition();

            this._direction = Vector3.zero;
        }

        public void UpdateTarget(Transform newTarget)
        {
            this.target = newTarget;
        }

        /// <summary>
        /// Set the ai car speed to a random value between a min and a max set in the game config.
        /// </summary>
        public void SetSpeed()
        {
            this._speed = Registry.gameConfig.aiMovementSpeed.GetValue();
        }
    }
}