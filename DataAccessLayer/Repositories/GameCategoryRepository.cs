using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class GameCategoryRepository(AppDbContext appDb) 
    : Repository<GameCategory>(appDb), IGameCategoryInterface { }
