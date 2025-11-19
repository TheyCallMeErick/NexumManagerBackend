namespace Application.DTOs.Outputs; 

public class PaginationResultDto
{
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }

    public PaginationResultDto(int totalItems, int totalPages, int currentPage, int pageSize)
    {
        TotalItems = totalItems;
        TotalPages = totalPages;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    public static PaginationResultDto Create(int totalItems, int currentPage, int pageSize)
    {
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return new PaginationResultDto(totalItems, totalPages, currentPage, pageSize);
    }
}