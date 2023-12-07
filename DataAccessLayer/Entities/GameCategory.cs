namespace DataAccessLayer.Entities;

public class GameCategory : BaseEntity
{
    public List<Game> Games { get; set; } = new List<Game>();
}
