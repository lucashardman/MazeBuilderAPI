namespace MazeBuilderAPI.Algorithms.Maze;

using Interfaces;
using Models.Enums;
using Models.Internal;
using Models.Responses;

public abstract class MazeBuilderBaseAlgorithm : IMazeStrategy
{
    public abstract MazeAlgorithm MazeAlgorithmName { get; }
    protected abstract bool ShouldInitializeWalls { get; }
    public abstract void Generate();
    protected List<List<MazeVertex>>? Maze { get; set; }
    
    // Use this array to move between vertex (up, down, left and right)
    protected readonly List<IntPoint> Directions =
    [
        new(0, -1),
        new(0, 1),
        new(-1, 0),
        new(1, 0)
    ];
    
    protected int Rows { get; private set; }
    protected int Columns { get; private set; }
    private int Seed { get; set; }
    protected Random RandomStream { get; private set; } = new();

    // Get vertex index using (x, y) coordinates
    protected int GetVertexIndex(int x, int y) => y * Rows + x;
        
    // Check if vertex exists
    protected bool IsValidVertex(int x, int y) => x >= 0 && x < Rows && y >= 0 && y < Columns;

    public bool Initialize(int height, int width, int seed = -1)
    {
        if (seed == -1)
        {
            var randomStream = new Random();
            seed = randomStream.Next(1, int.MaxValue);
        }

        RandomStream = new Random(seed);
        Columns = height;
        Rows = width;
        Seed = seed;

        return InitializeBoard();
    }
    
    // Connect two vertex setting the edge between them to true
    protected void HandleWallBetween(int x1, int y1, int x2, int y2, bool bRemoveWall)
    {
        if (Maze is null) return;
            
        if (!IsValidVertex(x1, y1) || !IsValidVertex(x2, y2)) return;
        
        if (x1 == x2)
        {
            if (y1 < y2)
            {
                Maze[y1][x1].DownEdge = bRemoveWall;
                Maze[y2][x2].UpEdge = bRemoveWall;
            }
            else
            {
                Maze[y1][x1].UpEdge = bRemoveWall;
                Maze[y2][x2].DownEdge = bRemoveWall;
            }
        }
        else if (y1 == y2)
        {
            if (x1 < x2)
            {
                Maze[y1][x1].RightEdge = bRemoveWall;
                Maze[y2][x2].LeftEdge = bRemoveWall;
            }
            else
            {
                Maze[y1][x1].LeftEdge = bRemoveWall;
                Maze[y2][x2].RightEdge = bRemoveWall;
            }
        }
    }
    
    private bool InitializeBoard()
    {
        if (Rows == 0 || Columns == 0) return false;
        
        Maze = [];
        
        for (var i = 0; i < Columns; i++)
        {
            Maze.Add([]);
            for (var j = 0; j < Rows; j++)
            {
                Maze[i].Add(ShouldInitializeWalls
                    ? new MazeVertex(false, false, false, false)
                    : new MazeVertex(true, true, true, true));
            }
        }
        if (!ShouldInitializeWalls) // Add the maze limits if it's initialized with no wall
        {
            for (var i = 0; i < Rows; i++)
            {
                Maze[0][i].UpEdge = false;
                Maze[Columns - 1][i].DownEdge = false;
            }
            for (var i = 0; i < Columns; i++)
            {
                Maze[i][0].LeftEdge = false;
                Maze[i][Rows - 1].RightEdge = false;
            }
        }
        return true;
    }

    /*
     * Convert the Maze class to the response class of the API.
     */
    public MazeResponse ConvertMazeToResponseType()
    {
        Maze ??= [];
        var response = new MazeResponse
        {
            Maze = Maze,
            Seed = Seed,
            Rows = Rows,
            Columns = Columns,
            Algorithm = MazeAlgorithmName
        };
    
        return response;
    }
}