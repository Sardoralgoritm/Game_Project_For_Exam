using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UnitOfWork(AppDbContext appDb,
                        IGameCategoryInterface gameCategory,
                        IGameInterface game) : IUnitOfWork
{
    private readonly AppDbContext _appDb = appDb;

    public IGameCategoryInterface GameCategory { get; } = gameCategory;

    public IGameInterface Game { get; } = game;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
        => await _appDb.SaveChangesAsync();
}
