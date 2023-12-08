namespace DataAccessLayer.Exceptions;

public class GameException(string errorMessage) : Exception
{
    public readonly string errorMessage = errorMessage;
}
