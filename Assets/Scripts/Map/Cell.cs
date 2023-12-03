using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;

        [HideInInspector] public PointType type = PointType.MID;

        private void OnTriggerEnter(Collider other)
        {
            // exit, if the collided gameobject isn't the car
            if (other.TryGetComponent<ManagerCar>(out var car) && car == null) return;

            // exit, if the cell isn't the last one
            if (type != PointType.END) return;
        
        
            Debug.Log("CELL OnTrigger(): player's car successfully finished the circuit");
        }

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);

        public void SetType(PointType type) => this.type = type;
    }
}