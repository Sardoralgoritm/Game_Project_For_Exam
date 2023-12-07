namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IGameCategoryInterface GameCategory { get; }
    public IGameInterface Game { get; } 
    Task SaveAsync();
}
