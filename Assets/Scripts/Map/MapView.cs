using UnityEngine;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        private Map _map;

        public void ShowMap(Map map)
        {
            if (map == null)
            {
                Debug.LogError("MAP VIEW: this map is null");
                return;
            }

            _map = map;

            GenerateCells();
        }
    
    
        /// <summary>
        /// Generates a circuit as a list of cell of a size based on the map config
        /// Clears walls on map to display the race circuit and hides unused cells 
        /// </summary>
        private void GenerateCells()
        {
            for (int index = 0; index < _map.circuit.Count; index++)
            {
                // update graphics display
                var cellPos = new Vector3(_map.circuit[index].x, 0, _map.circuit[index].z) * Registry.mapConfig.sizeScaler;
                _map.circuit[index].graphics = Instantiate(Registry.mapConfig.cellPrefab, cellPos, Quaternion.identity, transform);
                _map.circuit[index].graphics.transform.localScale *= Registry.mapConfig.sizeScaler;
            
                // set cell placement informations
                if (index == _map.circuit.Count - 1) _map.circuit[index].graphics.isFinishCell = true;
            }

            foreach (Point point in _map.circuit)
            {
                // clear walls of selected cells
                ClearWalls(point.previous, point);
            }
        }
    
    
        /// <summary>
        /// Clears walls that obstruct other cells
        /// </summary>
        private static void ClearWalls(Point previousPoint, Point currentPoint)
        {
            // exit, if there is no previous point graphics 
            if (previousPoint == null) return;

            if (previousPoint.graphics.transform.position.x < currentPoint.graphics.transform.position.x)
            {
                previousPoint.graphics.ClearRightWall();
                currentPoint.graphics.ClearLeftWall();
                return;
            }

            if (previousPoint.graphics.transform.position.x > currentPoint.graphics.transform.position.x)
            {
                previousPoint.graphics.ClearLeftWall();
                currentPoint.graphics.ClearRightWall();
                return;
            }

            if (previousPoint.graphics.transform.position.z < currentPoint.graphics.transform.position.z)
            {
                previousPoint.graphics.ClearFrontWall();
                currentPoint.graphics.ClearBackWall();
                return;
            }

            if (previousPoint.graphics.transform.position.z > currentPoint.graphics.transform.position.z)
            {
                previousPoint.graphics.ClearBackWall();
                currentPoint.graphics.ClearFrontWall();
            }
        }
    }
}
