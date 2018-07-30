namespace Alpha.Services.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
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

        [HttpGet("{custId}/messages/{messageType}/{language}/{fromTime}/{toTime}")]
        public async Task<IActionResult> GetMessages(
            int custId = 1176086,
            int messageType = 2,
            string language = "en-US",
            long fromTime = 1532490957191,
            long toTime = 1532922530926)
        {
            var messages = await mediator.Send(
                new MessageQuery(
                    custId,
                    messageType,
                    language,
                    DateTimeOffset.FromUnixTimeMilliseconds(fromTime).DateTime.ToLocalTime(),
                    DateTimeOffset.FromUnixTimeMilliseconds(toTime).DateTime.ToLocalTime()));

            return Ok(messages);
        }
    }
}