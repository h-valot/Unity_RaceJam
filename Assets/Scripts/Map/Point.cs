using UnityEngine;

[System.Serializable]
public class Point
{
    public int x, z;
    public Point previous;
    public bool isVisited;
    public int place;
    public PointType type;

    [HideInInspector] public Cell graphics;
    
    public Point(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public void Visit() => isVisited = true;

    public void SetType(PointType type) => this.type = type;
}

public enum PointType
{
    START = 0,
    MID,
    END
}