using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Algorithms.Maze;
using MazeBuilderAPI.Models.Internal;
using MazeBuilderAPI.Models.Responses;

namespace MazeBuilderAPI.Controllers;


public class MazeController : MazeBuilderBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Get([Required]int height, [Required] int width, [Required] MazeAlgorithm mazeAlgorithm, int seed = -1)
    {
        switch (mazeAlgorithm)
        {
            case MazeAlgorithm.HuntAndKill:
            {
                var huntAndKill = new HuntAndKillAlgorithm(height, width);
                huntAndKill.Run(seed);
                return Ok(huntAndKill.ConvertMazeToResponseType());
            }
            case MazeAlgorithm.RecursiveDivision:
                var recursiveDivision = new RecursiveDivision(height, width);
                recursiveDivision.Run(seed);
                return Ok(recursiveDivision.ConvertMazeToResponseType());
            case MazeAlgorithm.SimplifiedPrim:
                var simplifiedPrim = new SimplifiedPrim(height, width);
                simplifiedPrim.Run(seed);
                return Ok(simplifiedPrim.ConvertMazeToResponseType());
            case MazeAlgorithm.Eller:
                var eller = new EllerAlgorithm(height, width);
                eller.Run(seed);
                return Ok(eller.ConvertMazeToResponseType());
            case MazeAlgorithm.BinaryTree:
                var binaryTree = new BinaryTreeAlgorithm(height, width);
                binaryTree.Run(seed);
                return Ok(binaryTree.ConvertMazeToResponseType());
            case MazeAlgorithm.RandomizedKruskal:
                break;
            case MazeAlgorithm.Sidewinder:
                break;
            case MazeAlgorithm.AldousBroder:
                break;
            case MazeAlgorithm.DepthFirstSearch:
                var depthFirstSearch = new DepthFirstSearch(height, width);
                depthFirstSearch.Run(seed);
                return Ok(depthFirstSearch.ConvertMazeToResponseType());
            default:
                break;
        }
        return Ok();
    }
}