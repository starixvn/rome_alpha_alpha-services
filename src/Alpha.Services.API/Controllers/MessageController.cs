namespace Alpha.Services.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [FormatFilter]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "HMAC")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/messages/{messageType}/{language}/{fromTime}/{toTime}")]
        public async Task<IActionResult> GetMessages(
            int custId,
            int messageType,
            string language,
            long fromTime,
            long toTime)
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