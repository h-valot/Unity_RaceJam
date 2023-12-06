using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("REFERENCE")] 
        public MapView mapView;
        public MapGenerator mapGenerator;
    
        [HideInInspector] public Map currentMap;

        /// <summary>
        /// Generate a map of points and display those points as cells
        /// </summary>
        public void GenerateMap()
        {
            currentMap = mapGenerator.GetMap();
            mapView.ShowMap(currentMap, this);
        }
    }
}
