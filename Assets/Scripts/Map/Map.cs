using System.Collections.Generic;

[System.Serializable]
public class Map
{
    public List<Cell> circuit = new List<Cell>();
    
    public Map(List<Cell> circuit)
    {
        this.circuit = circuit;
    }
}