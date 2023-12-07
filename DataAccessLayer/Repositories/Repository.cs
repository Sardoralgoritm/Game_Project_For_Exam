using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class Repository<TEntity>(AppDbContext appDbContext) : IRepository<TEntity> where TEntity : class
{
    public DbSet<TEntity> _DbContext = appDbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
        => await _DbContext.AddAsync(entity);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _DbContext.Remove(entity);
    }

    public async Task<List<TEntity>> GetAllAsync()
        => await _DbContext.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id)
        => await _DbContext.FindAsync(id);

    public Task UpdateAsync(TEntity entity)
    {
        _DbContext.Update(entity);
        return Task.CompletedTask;
    }
}
