using UnityEngine;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public GameObject leftWall, rightWall, frontWall, backWall;
    
        [HideInInspector] public Cell previous;
        [HideInInspector] public bool isVisited;

        public void Visit() => isVisited = true;

        public void Hide()
        {
            ClearRightWall();
            ClearLeftWall();
            ClearFrontWall();
            ClearBackWall();
        }

        public void ClearLeftWall() => leftWall.SetActive(false);
        public void ClearRightWall() => rightWall.SetActive(false);
        public void ClearFrontWall() => frontWall.SetActive(false);
        public void ClearBackWall() => backWall.SetActive(false);
    }
}