namespace Alpha.Services.API.Controllers
{
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [FormatFilter]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "HMAC")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetMessages([FromBody]MessageQuery message)
        {
            var messages = await mediator.Send(message);

            return Ok(messages);
        }
    }
}