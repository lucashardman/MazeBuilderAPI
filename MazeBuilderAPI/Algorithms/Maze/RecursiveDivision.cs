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

        Divide(0, 0, Rows, Columns);
    }

    private void Divide(int minX, int minY, int maxX, int maxY)
    {
        // Stop if the area is too small
        if (maxX - minX < 2 || maxY - minY < 2) return;

        // Choose if it should divide horizontally or vertically
        var isHorizontal = ChooseOrientation(maxX - minX, maxY - minY);

        if (isHorizontal)
        {
            // Horizontal division
            var y = RandomStream.Next(minY + 1, maxY);
            var passageX = RandomStream.Next(minX, maxX);

            // Draw passage
            for (var x = minX; x < maxX; x++)
            {
                if (x != passageX)
                {
                    HandleWallBetween(x, y, x, y - 1, false);
                }
            }

            // Use recursion to divide the new areas
            Divide(minX, minY, maxX, y);       // Upper area
            Divide(minX, y, maxX, maxY);       // Lower area
        }
        else
        {
            // Vertical division
            var x = RandomStream.Next(minX + 1, maxX);
            var passageY = RandomStream.Next(minY, maxY);

            // Draw passage
            for (var y = minY; y < maxY; y++)
            {
                if (y != passageY)
                {
                    HandleWallBetween(x, y, x - 1, y, false);
                }
            }

            // Use recursion to divide the new areas
            Divide(minX, minY, x, maxY);       // Left area
            Divide(x, minY, maxX, maxY);       // Right area
        }
    }
    private bool ChooseOrientation(int width, int height)
    {
        if (width < height) return true; // Horizontal
        if (height < width) return false; // Vertical
        return RandomStream.Next(2) == 0; // Random
    }
}