namespace MazeBuilderAPI.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Responses;
using Services;

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
            if (mazeResponse == null) return BadRequest("Não foi possível gerar o labirinto com os parâmetros fornecidos.");
            return Ok(mazeResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}