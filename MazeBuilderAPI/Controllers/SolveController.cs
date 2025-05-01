using System.ComponentModel.DataAnnotations;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MazeBuilderAPI.Controllers;

public class SolveController :  MazeBuilderBaseController
{
    private readonly MazeService _mazeService;
    
    public SolveController(MazeService mazeService)
    {
        _mazeService = mazeService;
    }
    
    [HttpGet("solve")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Solve(
        [Required] int seed,
        [Required] int height, 
        [Required] int width, 
        [Required] MazeAlgorithm mazeAlgorithm, 
        [Required] PathfindingAlgorithm pathfindingAlgorithm
    )
    {
        try
        {
            var mazeResponse = _mazeService.Generate(height, width, mazeAlgorithm, seed);
            return Ok(mazeResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}