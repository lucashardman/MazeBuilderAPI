using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Algorithms.Maze;

namespace MazeBuilderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MazeController : ControllerBase
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
                HuntAndKillAlgorithm.Run(height, width, seed);
                break;
            }
            default:
            {
                break;
            }
        }
        return Ok("Maze Built");
    }
}