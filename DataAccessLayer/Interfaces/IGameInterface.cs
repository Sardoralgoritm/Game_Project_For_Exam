using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IGameInterface : IRepository<Game>
{
    List<Game> GetAllWithCategory(int id);
}
