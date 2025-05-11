namespace MazeBuilderAPI.Models.Responses;

using Internal;

public class SolveResponse
{
    public List<IntPoint> Path { get; set; }
    public int PathLength { get; set; } 
    
}