using Microsoft.AspNetCore.Mvc;
using SampleWebApplication1.Models;
using SampleWebApplication1.Services;

namespace SampleWebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(IChatbotService chatbotService, ILogger<ChatbotController> logger)
        {
            _chatbotService = chatbotService;
            _logger = logger;
        }

        [HttpPost("message")]
        public async Task<ActionResult<ChatbotResponse>> SendMessage([FromBody] ChatbotRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { message = "Message cannot be empty." });
            }

            try
            {
                var response = await _chatbotService.SendAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Chatbot service configuration or upstream request failed.");
                return StatusCode(StatusCodes.Status502BadGateway, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected chatbot error.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unexpected error while processing chatbot request." });
            }
        }
    }
}
