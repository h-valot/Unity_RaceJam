using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        private Point[,] _mapGrid;
        private List<Point> _circuit = new List<Point>();
        private readonly List<Point> _points = new List<Point>();

        /// <summary>
        /// Returns a randomly generated list of points as a map
        /// </summary>
        public Map GetMap()
        {
            InitializeMapGrid();
            GenerateMap(null, _mapGrid[0, 0]);
            SelectPoint();

            return new Map(_circuit);
        }

        /// <summary>
        /// Creates a map of points with a size register in map config
        /// </summary>
        private void InitializeMapGrid()
        {
            _mapGrid = new Point[Registry.mapConfig.mapSize, Registry.mapConfig.mapSize];
            for (int x = 0; x < Registry.mapConfig.mapSize; x++)
            {
                for (int z = 0; z < Registry.mapConfig.mapSize; z++)
                {
                    _mapGrid[x, z] = new Point(x, z);
                }
            }
        }

        /// <summary>
        /// Adds a point to the points list. Calls itself to add the next point.
        /// </summary>
        private void GenerateMap(Point previousPoint, Point currentPoint)
        {
            // update display of the current point
            currentPoint.Visit();
            currentPoint.previous = previousPoint;
            _points.Add(currentPoint);
    
            // generate next points
            Point nextPoint;
            do {
                nextPoint = GetNextUnvisitedPoint(currentPoint);
                if (nextPoint != null) GenerateMap(currentPoint, nextPoint);
            } while (nextPoint != null);
        }

        /// <summary>
        /// Returns a randomly selected point from the nearby points
        /// </summary>
        private Point GetNextUnvisitedPoint(Point currentPoint)
        {
            var unvisitedPoints = GetUnvisitedPoints(currentPoint);
            return unvisitedPoints.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
        }

        /// <summary>
        /// Returns a ienumerable list of points that contains every nearby unvisited points
        /// </summary>
        private IEnumerable<Point> GetUnvisitedPoints(Point currentPoint)
        {
            if (currentPoint.x + 1 < Registry.mapConfig.mapSize)
            {
                Point pointToRight = _mapGrid[currentPoint.x + 1, currentPoint.z];
                if (!pointToRight.isVisited) yield return pointToRight;
            }

            if (currentPoint.x - 1 >= 0)
            {
                Point pointToLeft = _mapGrid[currentPoint.x - 1, currentPoint.z];
                if (!pointToLeft.isVisited) yield return pointToLeft;
            }

            if (currentPoint.z + 1 < Registry.mapConfig.mapSize)
            {
                Point pointToFront = _mapGrid[currentPoint.x, currentPoint.z + 1];
                if (!pointToFront.isVisited) yield return pointToFront;
            }

            if (currentPoint.z - 1 >= 0)
            {
                Point pointToBack = _mapGrid[currentPoint.x, currentPoint.z - 1];
                if (!pointToBack.isVisited) yield return pointToBack;
            }
        }

        /// <summary>
        /// Generates a circuit as a list of point of a size based on the map config
        /// </summary>
        private void SelectPoint()
        {
            int circuitSize = Registry.mapConfig.circuitSize > _points.Count ? _points.Count : Registry.mapConfig.circuitSize;
            _circuit = _points.Take(circuitSize).ToList();
        }
    }
}