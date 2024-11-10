using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class MazeBuilderBaseAlgorithm
{
    protected List<List<MazeVertex>>? Maze { get; set; }
    
    // Use this array to move between vertex (up, down, left and right)
    protected List<IntPoint> Directions =
    [
        new IntPoint(0, -1),
        new IntPoint(0, 1),
        new IntPoint(-1, 0),
        new IntPoint(1, 0)
    ];
    
    protected int Rows { get; init; }
    protected int Columns { get; init; }

    // Get vertex index using (x, y) coordinates
    protected int GetVertexIndex(int x, int y) => y * Rows + x;
        
    // Check if vertex exists
    protected bool IsValidVertex(int x, int y) => x >= 0 && x < Rows && y >= 0 && y < Columns;
    
    // Connect two vertex setting the edge between them to true
    protected void RemoveWallBetween(int x1, int y1, int x2, int y2)
    {
        if (Maze is null) return;
            
        if (x1 == x2)
        {
            if (y1 < y2)
            {
                Maze[y1][x1].DownEdge = true;
                Maze[y2][x2].UpEdge = true;
            }
            else
            {
                Maze[y1][x1].UpEdge = true;
                Maze[y2][x2].DownEdge = true;
            }
        }
        else if (y1 == y2)
        {
            if (x1 < x2)
            {
                Maze[y1][x1].RightEdge = true;
                Maze[y2][x2].LeftEdge = true;
            }
            else
            {
                Maze[y1][x1].LeftEdge = true;
                Maze[y2][x2].RightEdge = true;
            }
        }
    }
    
    protected bool Initialize()
    {
        if (Rows == 0 || Columns == 0) return false;
        
        Maze = [];
        
        for (var i = 0; i < Columns; i++)
        {
            Maze.Add([]);
            for (var j = 0; j < Rows; j++)
            {
                Maze[i].Add(new MazeVertex(false, false, false, false));
            }
        }
        return true;
    }
    
    
    /*
     * Convert the Maze class to the response class of the API. If necessary, implement it.
     */
    public List<List<MazeVertex>>? ConvertMazeToResponseType()
    {
        return Maze;
    }
}