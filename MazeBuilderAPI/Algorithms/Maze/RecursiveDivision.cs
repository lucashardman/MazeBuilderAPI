namespace MazeBuilderAPI.Algorithms.Maze;

public class RecursiveDivision : MazeBuilderBaseAlgorithm
{
    private Random _randomStream;
    
    public RecursiveDivision(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
        _randomStream = new Random();
    }

    public void Run(int seed = -1)
    {
        if (Maze is null)
        {
            var bIsInitialized = Initialize(false);
            if (!bIsInitialized) return;
        }

        // Initializing the random stream
        if (seed != -1) _randomStream = new Random(seed);
        
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
            var y = _randomStream.Next(minY + 1, maxY);
            var passageX = _randomStream.Next(minX, maxX);

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
            var x = _randomStream.Next(minX + 1, maxX);
            var passageY = _randomStream.Next(minY, maxY);

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
        return _randomStream.Next(2) == 0; // Aleatório
    }
}