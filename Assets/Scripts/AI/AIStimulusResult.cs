using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    public class AIStimulusResult
    {
        public Transform sourceTransform;
        public List<Transform> otherTansforms = new List<Transform>();
        /// <summary>
        /// Contain any kind of value: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
        /// </summary>
        public List<object> objects = new List<object>();
    }
}