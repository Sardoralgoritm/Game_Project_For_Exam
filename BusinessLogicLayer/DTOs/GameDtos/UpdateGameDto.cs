namespace BusinessLogicLayer.DTOs.GameDtos;

public class UpdateGameDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Rate { get; set; }
    public string Company { get; set; } = string.Empty;
    public int GameCategoryId { get; set; }
}
