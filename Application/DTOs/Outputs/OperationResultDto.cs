namespace Application.DTOs.Outputs; 

public record OperationResultDto<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public PaginationResultDto? Pagination { get; init; }

    private OperationResultDto(bool success)
    {
        Success = success;
    }

    private OperationResultDto(bool success, string? message)
    {
        Success = success;
        Message = message;
    }

    public static OperationResultDto<T> SuccessResult()
    {
        return new OperationResultDto<T>(true);
    }

    public static OperationResultDto<T> FailureResult(string message)
    {
        return new OperationResultDto<T>(false, message);
    }

    public OperationResultDto<T> WithMessage(string? message)
    {
        return this with { Message = message };
    }

    public OperationResultDto<T> WithData(T? data)
    {
        return this with
        {
            Data = data
        };
    }

    public OperationResultDto<T> WithPagination(PaginationResultDto pagination)
    {
        return this with
        {
            Pagination = pagination
        };
    }
}

