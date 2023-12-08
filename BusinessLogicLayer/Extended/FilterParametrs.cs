namespace BusinessLogicLayer.Extended;

public record FilterParametrs(
    int pageSize,
    int pageNumber,
    string? name)
{
    public string Name = name ?? string.Empty;
    public int PageNumber = pageNumber;
    public int PageSize = pageSize;
}