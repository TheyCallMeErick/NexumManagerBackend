namespace Domain.Models; 

public class AuditLog
{
    public DateTime Timestamp { get; set; }
    public string Level { get; set; } = "Information";
    public string Event { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? EntityId { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public object? Context { get; set; } 
}
