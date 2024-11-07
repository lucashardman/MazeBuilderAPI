namespace MazeBuilderAPI.Models.Internal;

public struct IntPoint
{
    public int X { get; }
    public int Y { get; }

    public IntPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Override do ToString() para facilitar a visualização
    public override string ToString() => $"({X}, {Y})";
}