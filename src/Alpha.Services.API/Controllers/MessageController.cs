namespace Alpha.Services.API.Controllers
{
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [FormatFilter]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "HMAC")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/{custLevel}/messages/{messageType}/{language}/{fromTime}/{toTime}")]
        public async Task<IActionResult> GetMessages(
            int custId,
            byte custLevel,
            int messageType,
            string language,
            long fromTime,
            long toTime)
        {
            var messages = await mediator.Send(
                new MessageQuery(
                    custId,
                    custLevel,
                    messageType,
                    language,
                    fromTime.ToLocalTime(),
                    toTime.ToLocalTime()));

            return Ok(messages);
        }
    }
}