namespace MazeBuilderAPI.Models.Internal;

public class MazeVertex
{
    public bool UpEdge { get; set; } = false;
    public bool DownEdge { get; set; } = false;
    public bool LeftEdge { get; set; } = false;
    public bool RightEdge { get; set; } = false;

    MazeVertex(bool upEdge, bool downEdge, bool leftEdge, bool rightEdge)
    {
        UpEdge = upEdge;
        DownEdge = downEdge;
        LeftEdge = leftEdge;
        RightEdge = rightEdge;
    }
}