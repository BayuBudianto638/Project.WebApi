namespace MiniLMService.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Page { get; set; }
        public string ContentPage { get; set; } = string.Empty;
        public int Year { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Status { get; set; } = "Active"; 
    }
}
