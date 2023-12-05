using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;
        public bool isFinishCell;
        public int place;
        
        [HideInInspector] public MapManager mapManager;

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}