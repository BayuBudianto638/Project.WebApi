using MiniLMService.Models;
using MiniLMService.Services.KnowledgeServices.Dto;
using System.Text;
using System.Text.Json;

namespace MiniLMService.Services.KnowledgeServices
{
    public class WeaviateService : IWeaviateService
    {
        private static readonly HttpClient httpClient = new();
        private static string _weaviateUrl = "";

        public WeaviateService(IConfiguration config)
        {
            _weaviateUrl = config["WeaviateUrl"] ?? throw new InvalidOperationException("WeaviateUrl is not configured.");
        }

        public async Task CreateSchema()
        {
            var schema = new
            {
                @class = "Books",  
                description = "A book with metadata such as title, page, content, year, etc.",
                properties = new[]
                   {
                        new { name = "id", dataType = new[] { "string" } }, 
                        new { name = "title", dataType = new[] { "string" } },
                        new { name = "page", dataType = new[] { "int" } },
                        new { name = "contentpage", dataType = new[] { "string" } },
                        new { name = "year", dataType = new[] { "int" } },
                        new { name = "createdby", dataType = new[] { "string" } },
                        new { name = "status", dataType = new[] { "string" } }
                    },
                vectorizer = "text2vec-transformers"  
            };


            var json = System.Text.Json.JsonSerializer.Serialize(schema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/schema", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteArticlesByTitle(Guid articleId)
        {
            var deleteRequest = new
            {
                @class = "Article", 
                where = new
                {
                    path = new[] { "id" }, 
                    @operator = "Equal",    
                    valueString = articleId.ToString()  
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(deleteRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/batch/objects", content);
            response.EnsureSuccessStatusCode(); 

        }

        public async Task<Guid> GetArticleId()
        {
            var query = new
            {
                query = @"
                        {
                            Get {
                                Book {
                                    id
                                    title
                                    page
                                    contentpage
                                    year
                                    createdby
                                    status
                                }
                            }
                        }"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/graphql", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<QueryResult>(responseJson);

            return result!.Data.Get.Book[0].Id;

        }

        public async Task QueryData()
        {
            var query = new
            {
                query = @"
                        {
                            Get {
                                Book {
                                    id
                                    title
                                    page
                                    contentpage
                                    year
                                    createdby
                                    status
                                }
                            }
                        }"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/graphql", content);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
        }

        public async Task<string> QueryWeaviate(string query)
        {
            var searchQuery = new
            {
                query = $@"
                    {{
                        Get {{
                            Book(
                                nearText: {{
                                    concepts: [""{query}""]  
                                }}
                                ) {{
                                title
                                page
                                contentpage
                                year
                                createdby
                                status
                                _additional {{
                                    distance
                                }}
                            }}
                        }}
                    }}"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(searchQuery);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/graphql", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return responseJson;
        }

        public async Task UploadData(BookModel input)
        {
            var book = new
            {
                @class = "Book",  
                properties = new
                {
                    id = Guid.NewGuid().ToString(),  
                    title = input.Title,
                    page = input.Page,
                    contentpage = input.ContentPage,
                    year = input.Year,
                    createdby = input.CreatedBy,
                    status = input.Status ?? "Active"  
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_weaviateUrl}/objects", content);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
        }
    }
}
