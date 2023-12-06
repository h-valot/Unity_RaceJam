using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class Point
    {
        [HideInInspector] public int x, z;
        [HideInInspector] public Point previous;
        [HideInInspector] public bool isVisited;
        [HideInInspector] public Cell graphics;
    
        public Point(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        /// <summary>
        /// Sets isVisited to TRUE
        /// </summary>
        public void Visit() => isVisited = true;
    }
}