using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;
        public Transform closestToFinishCellPoint;
        public bool isFinishCell;
        public int place;

        private void OnTriggerEnter(Collider other)
        {
            // exit, if the collided gameobject isn't the car
            if (other.TryGetComponent<ManagerCar>(out var car) && car == null) return;

            if (!isFinishCell)
            {
                car.positionOnCircuit = place;
            }
            else
            {
                Events.onPlayerReachesEnd?.Invoke();
            }    
        }

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}