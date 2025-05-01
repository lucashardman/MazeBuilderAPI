using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Models.Responses;
using MazeBuilderAPI.Models.Internal;
public class MazeResponse
{
    public List<List<MazeVertex>> Maze { get; set; }
    public int Seed { get; set; } 
    public int Rows { get; set; }
    public int Columns { get; set; }
    public MazeAlgorithm Algorithm { get; set; }
    
}