namespace Application.DTOs.Inputs.Task;


public record UpdateTaskDto(string Title,Guid UserCreating, Guid TaskId, string? Description, DateTime?  StartDate, DateTime?  DeadLine, string? Priority, IEnumerable<Guid>? Tags, IEnumerable<Guid>? UsersAssigned  );