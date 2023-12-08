using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Extended;

namespace BusinessLogicLayer.Interfaces;

public interface IGameService
{
    Task<List<GameDto>> GetAllGamesAsync();
    Task<GameDto> GetGameByIdAsync(int id);
    Task<PagedList<GameDto>> GetPagedListAsync(int pageNumber, int pageSize);
    List<GameDto> GetAllThisId(int id);
    Task<PagedList<GameDto>> Filter(FilterParametrs parametrs);
    Task AddGameAsync(AddGameDto addGame);
    Task UpdateAsync(UpdateGameDto updateGame);
    Task DeleteAsync(int id);
}
