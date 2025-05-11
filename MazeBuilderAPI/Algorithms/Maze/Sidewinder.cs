namespace MazeBuilderAPI.Algorithms.Maze;

using Models.Enums;

public class Sidewinder : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.Sidewinder;
    protected override bool ShouldInitializeWalls => true;

    public override void Generate()
    {
        for (int y = 0; y < Columns; y++)
        {
            var run = new List<int>();

            for (int x = 0; x < Rows; x++)
            {
                run.Add(x);

                bool atEasternBoundary = (x == Rows - 1);
                bool atNorthernBoundary = (y == 0);
                bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && RandomStream.Next(2) == 0);

                if (shouldCloseOut)
                {
                    int member = run[RandomStream.Next(run.Count)];
                    if (!atNorthernBoundary)
                    {
                        HandleWallBetween(member, y, member, y - 1, true);
                    }

                    run.Clear();
                }
                else
                {
                    HandleWallBetween(x, y, x + 1, y, true);
                }
            }
        }
    }
}