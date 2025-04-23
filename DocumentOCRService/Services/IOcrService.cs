namespace DocumentOCRService.Services
{
    public interface IOcrService
    {
        Task<string> ReadTextFromImage(string imagePath);
        Task<string> ReadTextFromStreamAsync(Stream imageStream);
    }
}
