namespace   Domain.Models;

public class Attach : BaseModel
{
   public string FileName { get; set; } = string.Empty;
   public string StoredFileName { get; set; } = string.Empty;
   public string ContentType { get; set; } = string.Empty;
   public long FileSize { get; set; }
   public string StorageKey { get; set; } = string.Empty;
}
