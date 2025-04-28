using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLMService.Models;
using MiniLMService.Services.FileuploadServices;
using MiniLMService.Services.KnowledgeServices;
using System.Formats.Asn1;
using System.Globalization;

namespace MiniLMService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly IWeaviateService _weaviateAppService;
        private readonly IFileUploadAppService _fileUploadAppService;

        public KnowledgeController(IWeaviateService weaviateAppService, IFileUploadAppService fileUploadAppService)
        {
            _weaviateAppService = weaviateAppService;
            _fileUploadAppService = fileUploadAppService;
        }

        // <summary>
        /// Upload Data from CSV
        /// </summary>
        [HttpPost("UploadData")]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<BookMap>();

                var records = csv.GetRecords<BookModel>();

                foreach (var article in records)
                {
                    _weaviateAppService.UploadData(article);
                }
            }

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                createdBy = "User123",
                UploadedAt = DateTime.Now,
                WeaviateObjectId = "10001"
            };

            await _fileUploadAppService.SaveFileAsync(uploadedFile);

            return Ok("Data uploaded successfully.");
        }

        [HttpGet("GetListFiles")]
        public async Task<IActionResult> GetAllChatSessions()
        {
            var data = await _fileUploadAppService.GetAllFileAsync();
            return Ok(data);
        }

        private sealed class BookMap : ClassMap<BookModel>
        {
            public BookMap()
            {
                Map(m => m.Id).Name("id");
                Map(m => m.Title).Name("title");
                Map(m => m.Page).Name("page");
                Map(m => m.ContentPage).Name("contentPage");
                Map(m => m.Year).Name("year");
                Map(m => m.CreatedBy).Name("createdBy");
                Map(m => m.Status).Name("status");
            }
        }
    }
}
