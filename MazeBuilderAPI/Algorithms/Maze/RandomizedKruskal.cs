using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class RandomizedKruskal : MazeBuilderBaseAlgorithm
{
    public RandomizedKruskal(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
    }

    public void Run(int seed = -1)
    {
        if (Maze is null)
        {
            var bIsInitialized = Initialize(true);
            if (!bIsInitialized) return;
        }
    }
}