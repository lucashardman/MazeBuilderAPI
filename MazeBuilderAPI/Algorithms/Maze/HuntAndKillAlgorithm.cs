using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class HuntAndKillAlgorithm : MazeBuilderBaseAlgorithm
{
    public void Run(int height, int width, int seed = -1)
    {
        bool bIsInitialized = false;
        if (Maze is null) bIsInitialized = Initialize(height, width);

        if (bIsInitialized && Maze is not null) Maze[0][0].DownEdge = true;
    }
}