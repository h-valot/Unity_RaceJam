using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        [Header("REFERENCES")]
        public GameObject leftWall;
        public GameObject rightWall;
        public GameObject frontWall;
        public GameObject backWall;
        
        [HideInInspector] public bool isFinishCell;
        [HideInInspector] public int place;
        [HideInInspector] public MapManager mapManager;

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}