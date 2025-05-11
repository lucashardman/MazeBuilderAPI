namespace MazeBuilderAPI.Algorithms.Maze;

using Models.Enums;

public class Eller : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.Eller;
    protected override bool ShouldInitializeWalls => true;

    public override void Generate()
    {
        var sets = new Dictionary<int, int>();
        var nextSet = 1;

        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                if (!sets.ContainsKey(x))
                {
                    sets[x] = nextSet++;
                }

                if (x < Rows - 1 && (RandomStream.Next(2) == 0 || y == Columns - 1))
                {
                    if (!sets.ContainsKey(x + 1))
                    {
                        sets[x + 1] = nextSet++;
                    }

                    if (sets[x] != sets[x + 1])
                    {
                        HandleWallBetween(x, y, x + 1, y, true);
                        int oldSet = sets[x + 1];
                        for (int i = 0; i < Rows; i++)
                        {
                            if (sets.ContainsKey(i) && sets[i] == oldSet)
                            {
                                sets[i] = sets[x];
                            }
                        }
                    }
                }
            }
            if (y < Columns - 1)
            {
                var nextRowSets = new Dictionary<int, int>();
                for (int x = 0; x < Rows; x++)
                {
                    if (RandomStream.Next(2) == 0 || !nextRowSets.ContainsValue(sets[x]))
                    {
                        HandleWallBetween(x, y, x, y + 1, true);
                        nextRowSets[x] = sets[x];
                    }
                }
                sets = nextRowSets;
            }
        }
    }
}