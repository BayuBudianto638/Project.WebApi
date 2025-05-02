namespace MiniLMService.Models
{
    public class UploadedFile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string WeaviateObjectId { get; set; } = string.Empty;
    }
}
