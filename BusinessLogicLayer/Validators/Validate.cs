using BusinessLogicLayer.DTOs.GameCategoyDtos;
using BusinessLogicLayer.DTOs.GameDtos;

namespace BusinessLogicLayer.Validators;

public static class Validate
{
    // Validator, For AddGameDto
    public static bool IsValid(this AddGameDto dto)
    {
        if (dto == null || dto.Name.Length < 2)
        {
            return false;
        }
        return true;
    }

    // Validator, For UpdateGameDto
    public static bool IsValid(this UpdateGameDto dto)
    {
        if (dto == null || dto.Name.Length < 2)
        {
            return false;
        }
        return true;
    }

    // Validator, For AddGameCategoryDto
    public static bool IsValid(this AddGameCategoryDto dto)
    {
        {
            if (dto == null || dto.Name.Length < 2)
            {
                return false;
            }
            return true;
        }
    }

    // Validator, For UpdateGameCategoryDto
    public static bool IsValid(this UpdateGameCategoryDto dto)
    {
        {
            if (dto == null || dto.Name.Length < 2)
            {
                return false;
            }
            return true;
        }
    }

}
