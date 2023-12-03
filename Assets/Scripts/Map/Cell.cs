using Script.AI.Car;
using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;
        public bool isFinishCell;
        public int place;
        
        [HideInInspector] public MapManager mapManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isFinishCell) Events.onPlayerReachesEnd?.Invoke();
            }

            if (other.transform.parent.TryGetComponent<AICar>(out var aiCar) && aiCar != null)
            {
                aiCar.UpdateTarget(mapManager.currentMap.GetNextCellTransform(place, 1));
            }
        }

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}