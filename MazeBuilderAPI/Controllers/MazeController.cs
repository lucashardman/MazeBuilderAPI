﻿using System.ComponentModel.DataAnnotations;
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
                break;
            case MazeAlgorithm.RandomizedPrim:
                break;
            case MazeAlgorithm.Eller:
                break;
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
                break;
            default:
                break;
        }
        return Ok();
    }
}