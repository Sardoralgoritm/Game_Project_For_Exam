using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class GameRepository(AppDbContext appDb) : Repository<Game>(appDb), IGameInterface
{
    private readonly AppDbContext _appDb = appDb;

    public async Task<List<Game>> GetAllWithCattegory()
        => await _appDb.Games.Include(i => i.gameCategory).ToListAsync();
}
