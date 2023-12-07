using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpGet("/Game/Getall/")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _gameService.GetAllGamesAsync();
            return Ok(list);
        }

        [HttpPost("/game/add/")]
        public async Task<IActionResult> Post(AddGameDto addGame)
        {
            await _gameService.AddGameAsync(addGame);
            return Ok(addGame);
        }
    }
}
