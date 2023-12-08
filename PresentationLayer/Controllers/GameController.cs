using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpGet("/Game/getall/")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _gameService.GetAllGamesAsync();
            return Ok(list);
        }


        [HttpGet("/Game/get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _gameService.GetGameByIdAsync(id);
                return Ok(result);
            }
            catch (GameException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("/game/add/")]
        public async Task<IActionResult> Post(AddGameDto addGame)
        {
            try
            {
                await _gameService.AddGameAsync(addGame);
                return Ok("Game created");
            }
            catch (GameException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("/Game/update/")]
        public async Task<IActionResult> Put(UpdateGameDto updateGameDto)
        {
            try
            {
                await _gameService.UpdateAsync(updateGameDto);
                return Ok(updateGameDto);
            }
            catch (GameException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("/Game/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gameService.DeleteAsync(id);
                return Ok("User deleted");
            }
            catch (GameException ex)
            {
                return BadRequest(ex.errorMessage);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
