using BusinessLogicLayer.DTOs.GameCategoyDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IGameCategoryService
{
    Task<List<GameCategotyDto>> GetAllGamesCategoryAsync();
    Task<GameCategotyDto> GetGameCategoryByIdAsync(int id);
    Task AddGameCategoryAsync(AddGameCategoryDto addGameCategory);
    Task UpdateAsync(UpdateGameCategoryDto updateGameCategory);
    Task DeleteAsync(int id);
}
