using AutoMapper;
using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class GameService(IUnitOfWork unitOfWork,
                         IMapper mapper) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddGameAsync(AddGameDto addGame)
    { 
        if (addGame.IsValid())
        {
            var list = await _unitOfWork.GameCategory.GetAllAsync();
            if (list.Any(i => i.Id == addGame.GameCategoryId))
            {
                var game = _mapper.Map<Game>(addGame);
                game.gameCategory = null;
                await _unitOfWork.Game.AddAsync(game);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new GameException("GameCategoryId not found!");
            }
        }
        else
        {
            throw new GameException("Game is not Valid!");
        }
        
    }

    public async Task DeleteAsync(int id)
    {
        var gameDto = await GetGameByIdAsync(id);
        if (gameDto != null)
        {
            var game = _mapper.Map<Game>(gameDto);
            game.gameCategory = null;
            await _unitOfWork.Game.DeleteAsync(game);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new GameException("Game not found!");
        }
    }

    public async Task<List<GameDto>> GetAllGamesAsync()
    {
        var list = await _unitOfWork.Game.GetAllAsync();
        return list.Select(i => _mapper.Map<GameDto>(i)).ToList();
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        var game = await _unitOfWork.Game.GetByIdAsync(id);
        if (game != null)
        {
            return _mapper.Map<GameDto>(game);
        }
        else
        {
            throw new GameException("Game not found!");
        }

    }

    public List<GameDto> GetAllThisId(int id)
    {
        var test = _unitOfWork.Game.GetAllWithCategory(id);
        if (test != null)
        {
            return test.Select(i => _mapper.Map<GameDto>(i)).ToList();
        }
        else
        {
            throw new GameException("There are no games for this categoryId!");
        }
    }

    public async Task<PagedList<GameDto>> Filter(FilterParametrs parametrs)
    {
        var list = await _unitOfWork.Game.GetAllAsync();
        if (parametrs.Name is not "")
        {
            list = list.Where(i => i.Name.ToLower().Contains(parametrs.Name.ToLower())).ToList();
        }

        var result = list.Select(i => _mapper.Map<GameDto>(i)).ToList();

        PagedList<GameDto> pagedList = new(result, result.Count, parametrs.PageNumber, parametrs.PageSize);

        return pagedList.ToPagedList(result, parametrs.PageSize, parametrs.PageNumber);
    }

    public async Task UpdateAsync(UpdateGameDto updateGame)
    {
        if (updateGame.IsValid())
        {
            var list = await _unitOfWork.GameCategory.GetAllAsync();
            if (list.Any(i => i.Id == updateGame.GameCategoryId))
            {
                var game = _mapper.Map<Game>(updateGame);
                game.gameCategory = null;
                await _unitOfWork.Game.UpdateAsync(game);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new GameException("GameCategoryId not found!");
            }
        }
        else
        {
            throw new GameException("Game is not Valid!");
        }
    }

    public async Task<PagedList<GameDto>> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var list = await _unitOfWork.Game.GetAllAsync();
        var pagedList = new PagedList<GameDto>(list.Select(c => _mapper.Map<GameDto>(c)).ToList(),
                                                   list.Count(), pageNumber, pageSize);

        return pagedList.ToPagedList(pagedList.Data,
                                     pageSize,
                                     pageNumber);
    }
}
