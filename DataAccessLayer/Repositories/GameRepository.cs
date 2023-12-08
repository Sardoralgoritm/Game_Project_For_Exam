using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class GameRepository(AppDbContext appDb) : Repository<Game>(appDb), IGameInterface
{
    private readonly AppDbContext _appDb = appDb;

    public List<Game> GetAllWithCategory(int id)
        => _appDb.Games.Where(i => i.GameCategoryId == id).ToList();
}
