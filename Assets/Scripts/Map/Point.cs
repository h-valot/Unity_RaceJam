using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class Point
    {
        public int x, z;
        public Point previous;
        public bool isVisited;

        [HideInInspector] public Cell graphics;
    
        public Point(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public void Visit() => isVisited = true;
    }
}