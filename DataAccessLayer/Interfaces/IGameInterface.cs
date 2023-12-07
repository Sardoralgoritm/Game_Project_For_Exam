using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IGameInterface : IRepository<Game>
{
    Task<List<Game>> GetAllWithCattegory();
}
