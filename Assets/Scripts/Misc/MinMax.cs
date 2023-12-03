using UnityEngine;

namespace Misc
{
    [System.Serializable]
    public class IntMinMax
    {
        public int min, max;
        public int GetValue() => Random.Range(min, max - 1);
    }

    [System.Serializable]
    public class FloatMinMax
    {
        public float min, max;
        public float GetValue() => Random.Range(min, max);
    }
}