using Map;
using UnityEngine;

namespace AI.Car
{
    public class AICar : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private Rigidbody rigidBody;
        
        [Header("SETTINGS")]
        [SerializeField] private bool focusPlayer;
        
        private Transform _target;
        private Vector3 _direction;
        private float _speed;
        
        public void OnSightWall(AIStimulusResult aiStimulusResult)
        {
            foreach (Transform other in aiStimulusResult.otherTansforms)
            {
                this._direction -= other.position - this.transform.position;
            }

            this._direction = this._direction.normalized;
        }
        
        public void OnSightPlayer(AIStimulusResult aiStimulusResult)
        {
            // exit, if the ai doesn't focus the player
            if (!this.focusPlayer) return;
            
            foreach (Transform other in aiStimulusResult.otherTansforms)
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

        public void OnSightOtherEnemyCar(AIStimulusResult aiStimulusResult)
        {
            this.OnSightWall(aiStimulusResult);
        }

        private void FixedUpdate()
        {
            _direction = Vector3.zero;
            _direction += (_target.position - transform.position).normalized;
            _direction.y = 0f;

            rigidBody.velocity = _direction * _speed;
            LookAt(_direction);
        }

        private void LookAt(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        /// <summary>
        /// Sets ai direction target to the given one
        /// </summary>
        public void UpdateTarget(Transform newTarget)
        {
            _target = newTarget;
        }

        /// <summary>
        /// Sets the ai car speed to a random value between a min and a max set in the game config.
        /// </summary>
        public void SetSpeed()
        {
            _speed = Registry.gameConfig.aiMovementSpeed.GetValue();
        }

        private void OnTriggerEnter(Collider other)
        {
            // exit, if the collided object isn't a cell
            if (!other.TryGetComponent<Cell>(out var cell) || cell == null) return;
            
            // ends circuit if an ai ends it
            if (cell.isFinishCell)
            {
                Events.onCircuitEnded?.Invoke(EndSituation.AI_WINS);
                return;
            }
            
            UpdateTarget(cell.mapManager.currentMap.GetNextCellTransform(cell.place, 1));
        }
    }
}