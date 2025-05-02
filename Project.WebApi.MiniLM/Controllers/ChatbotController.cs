using LLama.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniLMService.Models;
using Project.WebApi.MiniLM.Services;

namespace Project.WebApi.MiniLM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(ILogger<ChatbotController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Send")]
        public Task<string> SendMessage([FromBody] SendMessageInput input, [FromServices] StatefulChatService _service)
        {
            return _service.Send(input);
        }

        [HttpPost("Send/Stream")]
        public async Task SendMessageStream([FromBody] SendMessageInput input, [FromServices] StatefulChatService _service, CancellationToken cancellationToken)
        {

            Response.ContentType = "text/event-stream";

            await foreach (var r in _service.SendStream(input))
            {
                await Response.WriteAsync("data:" + r + "\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }

            await Response.CompleteAsync();
        }

        [HttpPost("History")]
        public async Task<string> SendHistory([FromBody] HistoryInput input, [FromServices] StatelessChatService _service)
        {
            var history = new ChatHistory();

            var messages = input.Messages.Select(m => new ChatHistory.Message(Enum.Parse<AuthorRole>(m.Role), m.Content));

            history.Messages.AddRange(messages);

            return await _service.SendAsync(history);
        }
    }
}
