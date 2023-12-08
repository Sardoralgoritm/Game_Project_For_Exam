namespace BusinessLogicLayer.Exceptions;

public class GameCategoryException(string errorMessage) : Exception
{
    public readonly string errorMessage = errorMessage;
}
