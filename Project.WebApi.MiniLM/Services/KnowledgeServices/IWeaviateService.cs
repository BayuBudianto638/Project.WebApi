using MiniLMService.Models;

namespace MiniLMService.Services.KnowledgeServices
{
    public interface IWeaviateService
    {
        Task CreateSchema();
        Task UploadData(BookModel input);
        Task QueryData();
        Task DeleteArticlesByTitle(Guid articleId);
        Task<Guid> GetArticleId();
        Task<string> QueryWeaviate(string query);
    }
}
