using Domain.Enums;

namespace  Application.DTOs.Inputs.Task; 

public record CreateTaskDTO(string Title,Guid UserCreating, Guid ProjectId, string? Description, DateTime?  StartDate, DateTime?  DeadLine, ETaskPriority Priority, IEnumerable<Guid> Tags, List<Guid> UsersAssigned );