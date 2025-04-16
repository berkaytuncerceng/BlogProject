using Application.Abstract;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GeminiController : Controller
    {
        private readonly IGeminiService _geminiService;

        public GeminiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost]
        public async Task<IActionResult> Suggest([FromBody] SuggestRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("Metin boş olamaz");
            }

            var result = await _geminiService.SuggestContinuationAsync(request.Text, request.CurrentContent);
            return Content(result);
        }

        [HttpPost]
        public IActionResult ClearHistory()
        {
            _geminiService.ClearConversationHistory();
            return Ok();
        }
    }
}
