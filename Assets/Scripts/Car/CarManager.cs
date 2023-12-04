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
        
        public void OnCollisionEnter(Collision collision)
        {
            // exit, if the collided object isn't a wall
            if (!collision.gameObject.CompareTag("Wall")) return;

            carGraphics.AnimateExplosion();
            Events.onCircuitEnded?.Invoke(true);
        }
    }
}
