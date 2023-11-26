public class Cell
{
    public int x, y; 
    public bool isAvailable;

    public Cell(int x, int y, bool isAvailable)
    {
        this.x = x;
        this.y = y;
        this.isAvailable = isAvailable;
    }
}