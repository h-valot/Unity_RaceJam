using UnityEngine;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        private Map _map;
        private MapConfig _mapConfig;

        public void ShowMap(Map map, MapConfig mapConfig)
        {
            if (map == null)
            {
                Debug.LogError("MAP VIEW: this map is null");
                return;
            }

            _map = map;
            _mapConfig = mapConfig;

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
                var cellPos = new Vector3(_map.circuit[index].x, 0, _map.circuit[index].z) * _mapConfig.sizeScaler;
                _map.circuit[index].graphics = Instantiate(_mapConfig.cellPrefab, cellPos, Quaternion.identity, transform);
                _map.circuit[index].graphics.transform.localScale *= _mapConfig.sizeScaler;
            
                // set cell placement informations
                _map.circuit[index].graphics.place = index;
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
                currentPoint.graphics.closestToFinishCellPoint = currentPoint.graphics.leftWall.transform;
                return;
            }

            if (previousPoint.graphics.transform.position.x > currentPoint.graphics.transform.position.x)
            {
                previousPoint.graphics.ClearLeftWall();
                currentPoint.graphics.ClearRightWall();
                currentPoint.graphics.closestToFinishCellPoint = currentPoint.graphics.rightWall.transform;
                return;
            }

            if (previousPoint.graphics.transform.position.z < currentPoint.graphics.transform.position.z)
            {
                previousPoint.graphics.ClearFrontWall();
                currentPoint.graphics.ClearBackWall();
                currentPoint.graphics.closestToFinishCellPoint = currentPoint.graphics.backWall.transform;
                return;
            }

            if (previousPoint.graphics.transform.position.z > currentPoint.graphics.transform.position.z)
            {
                previousPoint.graphics.ClearBackWall();
                currentPoint.graphics.ClearFrontWall();
                currentPoint.graphics.closestToFinishCellPoint = currentPoint.graphics.frontWall.transform;
            }
        }
    }
}
