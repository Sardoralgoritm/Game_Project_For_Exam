using BusinessLogicLayer.DTOs.GameDtos;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Validators;

public static class Validate
{
    public static bool IsValid(this AddGameDto dto)
    {
        if (dto == null || dto.Name.Length < 4 || dto.Price < 1 || dto.GameCategoryId == 0)
        {
            return false;
        }
        return true;
    }

    public static bool IsValid(this UpdateGameDto dto)
    {
        if (dto == null || dto.Name.Length < 4 || dto.Price < 1 || dto.GameCategoryId == 0)
        {
            return false;
        }
        return true;
    }
}
