namespace Alpha.Services.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet("messages/{messageType}")]
        public async Task<IActionResult> GetMessages(int custId, int messageType)
        {
            return Ok();
        }
    }
}