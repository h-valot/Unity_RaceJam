using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static MapConfig _mapConfig;
        private static Point[,] _mapGrid;
        private static List<Point> _circuit = new List<Point>();
        private static readonly List<Point> _points = new List<Point>();

        public static Map GetMap(MapConfig mapConfig)
        {
            if (mapConfig == null)
            {
                Debug.LogError("MAP GENERATOR: map config is null");
                return null;
            }

            _mapConfig = mapConfig;
        
            InitializeMapGrid();
            GenerateMap(null, _mapGrid[0, 0]);
            SelectPoint();

            return new Map(_circuit);
        }

        /// <summary>
        /// Creates a map of points with a size register in map config
        /// </summary>
        private static void InitializeMapGrid()
        {
            _mapGrid = new Point[_mapConfig.mapSize, _mapConfig.mapSize];
            for (int x = 0; x < _mapConfig.mapSize; x++)
            {
                for (int z = 0; z < _mapConfig.mapSize; z++)
                {
                    _mapGrid[x, z] = new Point(x, z);
                }
            }
        }

        /// <summary>
        /// Adds a point to the points list. Calls itself to add the next point.
        /// </summary>
        private static void GenerateMap(Point previousPoint, Point currentPoint)
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
        private static Point GetNextUnvisitedPoint(Point currentPoint)
        {
            var unvisitedPoints = GetUnvisitedPoints(currentPoint);
            return unvisitedPoints.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
        }

        /// <summary>
        /// Returns a ienumerable list of points that contains every nearby unvisited points
        /// </summary>
        private static IEnumerable<Point> GetUnvisitedPoints(Point currentPoint)
        {
            if (currentPoint.x + 1 < _mapConfig.mapSize)
            {
                Point pointToRight = _mapGrid[currentPoint.x + 1, currentPoint.z];
                if (!pointToRight.isVisited) yield return pointToRight;
            }

            if (currentPoint.x - 1 >= 0)
            {
                Point pointToLeft = _mapGrid[currentPoint.x - 1, currentPoint.z];
                if (!pointToLeft.isVisited) yield return pointToLeft;
            }

            if (currentPoint.z + 1 < _mapConfig.mapSize)
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
        private static void SelectPoint()
        {
            int circuitSize = _mapConfig.circuitSize > _points.Count ? _points.Count : _mapConfig.circuitSize;
            _circuit = _points.Take(circuitSize).ToList();
        }
    }
}