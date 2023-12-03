using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;
        public bool isFinishCell;

        private void OnTriggerEnter(Collider other)
        {
            // exit, if the collided gameobject isn't the car
            if (other.TryGetComponent<ManagerCar>(out var car) && car == null) return;

            // exit, if this is not the finish line cell
            if (!isFinishCell) return;

            Events.onPlayerReachesEnd?.Invoke();
        }

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}