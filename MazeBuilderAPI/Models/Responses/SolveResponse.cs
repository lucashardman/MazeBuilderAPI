namespace MazeBuilderAPI.Models.Responses;
using MazeBuilderAPI.Models.Internal;

public class SolveResponse
{
    public List<IntPoint> Path { get; set; }
    public int PathLength { get; set; } 
    
}