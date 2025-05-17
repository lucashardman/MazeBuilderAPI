namespace MazeBuilderAPI.Models.Internal;
public readonly struct IntPoint(int x, int y) : IEquatable<IntPoint>
{
    public int X { get;} = x;
    public int Y { get; } = y;

    public bool Equals(IntPoint other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) =>
        obj is IntPoint point && Equals(point);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(IntPoint left, IntPoint right) => left.Equals(right);
    public static bool operator !=(IntPoint left, IntPoint right) => !(left == right);
}