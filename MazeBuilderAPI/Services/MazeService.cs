using MazeBuilderAPI.Algorithms.Maze;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Responses;

namespace MazeBuilderAPI.Services;

public class MazeService
{
    public MazeResponse? Generate(int height, int width, MazeAlgorithm mazeAlgorithm, int seed)
    {
         if (seed == -1) 
         {
            Random random = new Random();
            seed = random.Next(1, int.MaxValue);
        }
        switch (mazeAlgorithm)
        {
            case MazeAlgorithm.HuntAndKill:
            {
                var huntAndKill = new HuntAndKillAlgorithm(height, width, seed, mazeAlgorithm);
                huntAndKill.Run(seed);
                return huntAndKill.ConvertMazeToResponseType();
            }
            case MazeAlgorithm.RecursiveDivision:
                var recursiveDivision = new RecursiveDivision(height, width, seed, mazeAlgorithm);
                recursiveDivision.Run(seed);
                return recursiveDivision.ConvertMazeToResponseType();
            case MazeAlgorithm.SimplifiedPrim:
                var simplifiedPrim = new SimplifiedPrim(height, width, seed, mazeAlgorithm);
                simplifiedPrim.Run(seed);
                return simplifiedPrim.ConvertMazeToResponseType();
            case MazeAlgorithm.Eller:
                var eller = new EllerAlgorithm(height, width, seed, mazeAlgorithm);
                eller.Run(seed);
                return eller.ConvertMazeToResponseType();
            case MazeAlgorithm.BinaryTree:
                var binaryTree = new BinaryTreeAlgorithm(height, width, seed, mazeAlgorithm);
                binaryTree.Run(seed);
                return binaryTree.ConvertMazeToResponseType();
            case MazeAlgorithm.RandomizedKruskal:
                var randomizedKruskal = new RandomizedKruskal(height, width, seed, mazeAlgorithm);
                randomizedKruskal.Run(seed);
                return randomizedKruskal.ConvertMazeToResponseType();
            case MazeAlgorithm.Sidewinder:
                var sidewinder = new Sidewinder(height, width, seed, mazeAlgorithm);
                sidewinder.Run(seed);
                return sidewinder.ConvertMazeToResponseType();
            case MazeAlgorithm.AldousBroder:
                var aldousBroder = new AldousBroder(height, width, seed, mazeAlgorithm);
                aldousBroder.Run(seed);
                return aldousBroder.ConvertMazeToResponseType();
            case MazeAlgorithm.DepthFirstSearch:
                var depthFirstSearch = new DepthFirstSearch(height, width, seed, mazeAlgorithm);
                depthFirstSearch.Run(seed);
                return depthFirstSearch.ConvertMazeToResponseType();
            default:
                throw new ArgumentException("Algoritmo de labirinto não suportado", nameof(mazeAlgorithm));
        }
    }
}