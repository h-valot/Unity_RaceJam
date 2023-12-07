using System;
using Map;
using UnityEngine;

namespace Car
{
    public class CarManager : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private InputPlayer inputPlayer;
        [SerializeField] private CarMovement carMovement;
        [SerializeField] public CarGraphics carGraphics;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            carMovement.ReceiveInput(inputPlayer.horizontalInput, inputPlayer.verticalInput);
            carGraphics.ToggleAccelerationParticules(inputPlayer.DoGatherInput());
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            // handle wall collision
            if (collision.gameObject.CompareTag("Wall"))
            {
                carGraphics.AnimateExplosion();
                Events.onCircuitEnded?.Invoke(EndSituation.PLAYER_DEAD);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // handle get to the finish cell
            if (other.TryGetComponent<Cell>(out var cell) &&
                cell != null &&
                cell.isFinishCell)
            {
                Events.onCircuitEnded?.Invoke(EndSituation.PLAYER_WINS);
            }
        }


        public void DisplayPauseMenu()
        {
            _gameManager.DisplayPauseGame();
        }
    }
}