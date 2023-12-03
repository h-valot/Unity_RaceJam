using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
    public List<Cell> circuit = new List<Cell>();
    
    public Map(List<Cell> circuit)
    {
        this.circuit = circuit;
    }
    
    public Transform GetFinishCellTransform()
    {
        return circuit[^1].transform;
    }
}