using AutoMapper;
using BusinessLogicLayer.DTOs.GameDtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class GameService(IUnitOfWork unitOfWork,
                         IMapper mapper) : IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddGameAsync(AddGameDto addGame)
    {
        var game = _mapper.Map<Game>(addGame);
        game.gameCategory = null;
        await _unitOfWork.Game.AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GameDto>> GetAllGamesAsync()
    {
        var cur = await _unitOfWork.Game.GetAllAsync();
        return cur.Select(i => _mapper.Map<GameDto>(i)).ToList();
    }

    public Task<GameDto> GetGameByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateGameDto updateGame)
    {
        throw new NotImplementedException();
    }
}
