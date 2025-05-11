using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Algorithms.Maze;

public class RecursiveDivision : MazeBuilderBaseAlgorithm
{

    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.RecursiveDivision;

    public override void Generate()
    {
        if (Maze is null)
        {
            var bIsInitialized = InitializeBoard(false);
            if (!bIsInitialized) return;
        }

        var randomStream = Seed == -1 ? new Random() : new Random(Seed);
        Divide(0, 0, Rows, Columns, randomStream);
    }

    private void Divide(int minX, int minY, int maxX, int maxY, Random randomStream)
    {
        // Stop if the area is too small
        if (maxX - minX < 2 || maxY - minY < 2) return;

        // Choose if it should divide horizontally or vertically
        var isHorizontal = ChooseOrientation(maxX - minX, maxY - minY, randomStream);

        if (isHorizontal)
        {
            // Horizontal division
            var y = randomStream.Next(minY + 1, maxY);
            var passageX = randomStream.Next(minX, maxX);

            // Draw passage
            for (var x = minX; x < maxX; x++)
            {
                if (x != passageX)
                {
                    HandleWallBetween(x, y, x, y - 1, false);
                }
            }

            // Use recursion to divide the new areas
            Divide(minX, minY, maxX, y, randomStream);       // Upper area
            Divide(minX, y, maxX, maxY, randomStream);       // Lower area
        }
        else
        {
            // Vertical division
            var x = randomStream.Next(minX + 1, maxX);
            var passageY = randomStream.Next(minY, maxY);

            // Draw passage
            for (var y = minY; y < maxY; y++)
            {
                if (y != passageY)
                {
                    HandleWallBetween(x, y, x - 1, y, false);
                }
            }

            // Use recursion to divide the new areas
            Divide(minX, minY, x, maxY, randomStream);       // Left area
            Divide(x, minY, maxX, maxY, randomStream);       // Right area
        }
    }
    private bool ChooseOrientation(int width, int height, Random randomStream)
    {
        if (width < height) return true; // Horizontal
        if (height < width) return false; // Vertical
        return randomStream.Next(2) == 0; // Random
    }
}