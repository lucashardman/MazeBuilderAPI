using MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Responses;

namespace MazeBuilderAPI.Services;

public class MazeService(IMazeAlgorithmFactory factory)
{
    public MazeResponse? Generate(int height, int width, MazeAlgorithm mazeAlgorithmName, int seed)
    {
        if (seed == -1)
        {
            Random random = new Random();
            seed = random.Next(1, int.MaxValue);
        }

        var mazeAlgorithm = factory.GetAlgorithm(mazeAlgorithmName);
        mazeAlgorithm.Initialize(height, width, seed);
        mazeAlgorithm.Generate();
        return mazeAlgorithm.ConvertMazeToResponseType();
    }
}