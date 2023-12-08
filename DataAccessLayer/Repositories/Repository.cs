using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class Repository<TEntity>(AppDbContext appDbContext) : IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> _DbContext = appDbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
        => await _DbContext.AddAsync(entity);

    public Task DeleteAsync(TEntity entity)
    {
        _DbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<TEntity>> GetAllAsync()
        => await _DbContext.AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id)
        => await _DbContext.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

    public Task UpdateAsync(TEntity entity)
    {
        _DbContext.Update(entity);
        return Task.CompletedTask;
    }
}
