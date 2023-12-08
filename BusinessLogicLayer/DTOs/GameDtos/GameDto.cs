namespace BusinessLogicLayer.DTOs.GameDtos;

public class GameDto : BaseDto
{
    public double Price { get; set; }
    public int Rate { get; set; }
    public string Company { get; set; } = string.Empty;
    public int GameCategoryId { get; set; }
}
