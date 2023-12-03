using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("REFERENCE")] 
        public MapConfig mapConfig;
        public MapView mapView;
    
        public Map currentMap;
    
        /// <summary>
        /// Generate a map of points and display those points as cells
        /// </summary>
        public void GenerateMap()
        {
            currentMap = MapGenerator.GetMap(mapConfig);
            mapView.ShowMap(currentMap, mapConfig);
        }
    }
}
