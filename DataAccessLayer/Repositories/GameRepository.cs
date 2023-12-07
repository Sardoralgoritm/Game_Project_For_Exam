using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class GameRepository(AppDbContext appDb) 
    : Repository<Game>(appDb), IGameInterface { }
