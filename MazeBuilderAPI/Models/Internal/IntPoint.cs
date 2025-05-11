namespace MazeBuilderAPI.Models.Internal;

public class IntPoint(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public override string ToString() => $"({X}, {Y})";
}