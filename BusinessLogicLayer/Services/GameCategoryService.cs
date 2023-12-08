using AutoMapper;
using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Extended;
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

    public async Task<PagedList<GameCategotyDto>> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var list = await _unitOfWork.GameCategory.GetAllAsync();
        var pagedList = new PagedList<GameCategotyDto>(list.Select(c => _mapper.Map<GameCategotyDto>(c)).ToList(),
                                                   list.Count(), pageNumber, pageSize);

        return pagedList.ToPagedList(pagedList.Data,
                                     pageSize,
                                     pageNumber);
    }

    public async Task<PagedList<GameCategotyDto>> Filter(FilterParametrs parametrs)
    {
        var list = await _unitOfWork.GameCategory.GetAllAsync();
        if (parametrs.Name is not "")
        {
            list = list.Where(i => i.Name.ToLower().Contains(parametrs.Name.ToLower())).ToList();
        }

        var result = list.Select(i => _mapper.Map<GameCategotyDto>(i)).ToList();

        PagedList<GameCategotyDto> pagedList = new(result, result.Count, parametrs.PageNumber, parametrs.PageSize);

        return pagedList.ToPagedList(result, parametrs.PageSize, parametrs.PageNumber);
    }

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
}
