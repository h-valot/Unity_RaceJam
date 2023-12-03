using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("REFERENCE")] 
        public MapView mapView;
    
        [Header("DEBUGGING")]
        public Map currentMap;
    
        /// <summary>
        /// Generate a map of points and display those points as cells
        /// </summary>
        public void GenerateMap()
        {
            currentMap = MapGenerator.GetMap();
            mapView.ShowMap(currentMap, this);
        }
    }
}
