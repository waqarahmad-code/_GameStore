using GameStore.Application.DTOs;
using GameStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameService _gameService;

    public GamesController(GameService gameService)
    {
        _gameService = gameService;
    }

    // GET: api/games
    // GET: api/games?pageNumber=1&pageSize=10
    // GET: api/games?search=action&pageNumber=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<GameDto>>> GetAll(
        [FromQuery] string? search = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _gameService.SearchAsync(
            search,
            pageNumber,
            pageSize);

        return Ok(result);
    }


    // GET: api/games/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameDto>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid game ID.");
        }

        var game = await _gameService.GetByIdAsync(id);

        if (game == null)
        {
            return NotFound(
                $"Game with ID {id} was not found.");
        }

        return Ok(game);
    }


    // POST: api/games
    [HttpPost]
    public async Task<ActionResult<GameDto>> Create(
        [FromBody] CreateGameRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var game = await _gameService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = game.Id },
            game);
    }


    // PUT: api/games/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<GameDto>> Update(
        int id,
        [FromBody] UpdateGameRequest request)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid game ID.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var game = await _gameService.UpdateAsync(
            id,
            request);

        if (game == null)
        {
            return NotFound(
                $"Game with ID {id} was not found.");
        }

        return Ok(game);
    }


    // DELETE: api/games/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid game ID.");
        }

        var game = await _gameService.GetByIdAsync(id);

        if (game == null)
        {
            return NotFound(
                $"Game with ID {id} was not found.");
        }

        await _gameService.DeleteAsync(id);

        return NoContent();
    }
}