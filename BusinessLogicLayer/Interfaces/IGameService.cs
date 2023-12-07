using BusinessLogicLayer.DTOs.GameDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Interfaces;

public interface IGameService
{
    Task<List<GameDto>> GetAllGamesAsync();
    Task<GameDto> GetGameByIdAsync(int id);
    Task AddGameAsync(AddGameDto addGame);
    Task UpdateAsync(UpdateGameDto updateGame);
    Task DeleteAsync(int id);
}
