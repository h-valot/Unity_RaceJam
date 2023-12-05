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

        void Update()
        {
            carMovement.ReceiveInput(inputPlayer.horizontalInput, inputPlayer.verticalInput);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            // handle wall collision
            if (collision.gameObject.CompareTag("Wall"))
            {
                carGraphics.AnimateExplosion();
                Events.onCircuitEnded?.Invoke(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // handle get to the finish cell
            if (other.TryGetComponent<Cell>(out var cell) &&
                cell != null &&
                cell.isFinishCell)
            {
                Events.onCircuitEnded?.Invoke(false);
            }
        }
    }
}
