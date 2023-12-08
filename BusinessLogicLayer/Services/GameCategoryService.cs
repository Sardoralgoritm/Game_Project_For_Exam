using AutoMapper;
using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class GameCategoryService(IUnitOfWork unitOfWork,
                                 IMapper mapper) : IGameCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddGameCategoryAsync(AddGameCategoryDto addGameCategory)
    {
        if (addGameCategory.IsValid())
        {
            var list = await GetAllGamesCategoryAsync();
            if (list.Any(i => i.Name == addGameCategory.Name))
            {
                throw new GameCategoryException("This Category already exist our Database!");
            }

            var gameCategory = _mapper.Map<GameCategory>(addGameCategory);
            await _unitOfWork.GameCategory.AddAsync(gameCategory);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GameCategoryException("AddGameCategory is not valid!");
        }
        
    }

    public async Task DeleteAsync(int id)
    {
        var gameCategoryDto = await GetGameCategoryByIdAsync(id);
        if (gameCategoryDto != null)
        {
            var gameCategory = _mapper.Map<GameCategory>(gameCategoryDto);
            await _unitOfWork.GameCategory.DeleteAsync(gameCategory);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GameCategoryException("GameCategory not found!");
        }
    }

    public async Task<List<GameCategotyDto>> GetAllGamesCategoryAsync()
    {
        var list = await _unitOfWork.GameCategory.GetAllAsync();
        return list.Select(i => _mapper.Map<GameCategotyDto>(i)).ToList();
    }

    public async Task<GameCategotyDto> GetGameCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.GameCategory.GetByIdAsync(id);
        if (category != null)
        {
            return _mapper.Map<GameCategotyDto>(category);
        }
        else
        {
            throw new GameCategoryException("GameCategory not found!");
        }
    }

    public async Task UpdateAsync(UpdateGameCategoryDto updateGameCategory)
    {
        if (updateGameCategory.IsValid())
        {
            var category = _mapper.Map<GameCategory>(updateGameCategory);
            await _unitOfWork.GameCategory.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GameCategoryException("GameCategory is not valid!");
        }
    }
}
