using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Algorithms.Maze;
using MazeBuilderAPI.Models.Internal;
using MazeBuilderAPI.Models.Responses;
using MazeBuilderAPI.Services;

namespace MazeBuilderAPI.Controllers;


public class MazeController(MazeService mazeService) : MazeBuilderBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(MazeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Get(
        [Required]int height,
        [Required] int width,
        [Required] MazeAlgorithm mazeAlgorithm,
        int seed = -1
    )
    {
        try
        {
            var mazeResponse = mazeService.Generate(height, width, mazeAlgorithm, seed);
            return Ok(mazeResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}