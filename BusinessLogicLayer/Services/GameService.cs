using AutoMapper;
using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System.Reflection;

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
        var cur = await _unitOfWork.Game.GetAllAsync();
        return cur.Select(i => _mapper.Map<GameDto>(i)).ToList();
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
}
