using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Algorithms.Maze;

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
                var maze = new HuntAndKillAlgorithm();
                maze.Run(height, width, seed);
                maze.ConvertMazeToResponseType();
                break;
            }
            case MazeAlgorithm.RecursiveDivision:
                break;
            case MazeAlgorithm.RandomizedPrim:
                break;
            case MazeAlgorithm.Eller:
                break;
            case MazeAlgorithm.BinaryTree:
                break;
            case MazeAlgorithm.RandomizedKruskal:
                break;
            case MazeAlgorithm.Sidewinder:
                break;
            case MazeAlgorithm.AldousBroder:
                break;
            case MazeAlgorithm.DepthFirstSearch:
                break;
        }
        return Ok("Maze Built");
    }
}