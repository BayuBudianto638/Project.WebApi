using MiniLMService.Models;

namespace MiniLMService.Services.FileuploadServices;

public interface IFileUploadAppService
{
    Task SaveFileAsync(UploadedFile message);
    Task<List<UploadedFile>> GetAllFileAsync();
    Task<List<UploadedFile>> GetFileByIdAsync(string id);
}