using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.Extended;

namespace BusinessLogicLayer.Interfaces;

public interface IGameCategoryService
{
    Task<List<GameCategotyDto>> GetAllGamesCategoryAsync();
    Task<GameCategotyDto> GetGameCategoryByIdAsync(int id);
    Task<PagedList<GameCategotyDto>> GetPagedListAsync(int pageNumber, int pageSize);
    Task<PagedList<GameCategotyDto>> Filter(FilterParametrs parametrs);
    Task AddGameCategoryAsync(AddGameCategoryDto addGameCategory);
    Task UpdateAsync(UpdateGameCategoryDto updateGameCategory);
    Task DeleteAsync(int id);
}
