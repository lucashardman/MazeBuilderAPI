using System.ComponentModel.DataAnnotations;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Responses;
using MazeBuilderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MazeBuilderAPI.Controllers;

public class SolveController(MazeService mazeService) : MazeBuilderBaseController
{
    private readonly SolveService _solveService = new();

    [HttpGet]
    [ProducesResponseType(typeof(SolveResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Get(
        [Required] int seed,
        [Required] int height, 
        [Required] int width, 
        [Required] MazeAlgorithm mazeAlgorithm, 
        [Required] PathfindingAlgorithm pathfindingAlgorithm
    )
    {
        try
        {
            var mazeResponse = mazeService.Generate(height, width, mazeAlgorithm, seed);
            if (mazeResponse == null)
            {
                return Problem();
            }
            var solveResponse = _solveService.Solve(mazeResponse.Maze, pathfindingAlgorithm);
            return Ok(solveResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}