using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly GPTService _gptService;

        public ConversationController(GPTService gptService)
        {
            _gptService = gptService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] ConversationModel conversationModel)
        {
            if (conversationModel?.Conversation == null)
                return BadRequest("Invalid model");

            foreach (var interaction in conversationModel.Conversation)
            {
                if (interaction.Agent != null && interaction.Customer != null)
                {
                    interaction.Analysis = await _gptService.GetAnalysisAsync(interaction.Agent, interaction.Customer);
                }
            }

            conversationModel.OverallSatisfaction = "This section is developing"; // Optionally, analyze overall satisfaction via GPT as well.
            return Ok(conversationModel);
        }
    }
}
