namespace MazeBuilderAPI.Services;

using Interfaces;
using Models.Enums;
using Models.Responses;

public class MazeService(IMazeAlgorithmFactory factory)
{
    public MazeResponse? Generate(int height, int width, MazeAlgorithm mazeAlgorithmName, int seed)
    {
        var mazeAlgorithm = factory.GetAlgorithm(mazeAlgorithmName);
        var bIsInitialized = mazeAlgorithm.Initialize(height, width, seed);
        if (!bIsInitialized) return null;

        mazeAlgorithm.Generate();
        return mazeAlgorithm.ConvertMazeToResponseType();
    }
}