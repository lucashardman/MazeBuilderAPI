using MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Responses;

namespace MazeBuilderAPI.Services;

public class MazeService(IMazeAlgorithmFactory factory)
{
    public MazeResponse? Generate(int height, int width, MazeAlgorithm mazeAlgorithmName, int seed)
    {
        var mazeAlgorithm = factory.GetAlgorithm(mazeAlgorithmName);
        mazeAlgorithm.Initialize(height, width, seed);
        mazeAlgorithm.Generate();
        return mazeAlgorithm.ConvertMazeToResponseType();
    }
}