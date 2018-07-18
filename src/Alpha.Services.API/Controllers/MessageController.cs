namespace Alpha.Services.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/messages/{messageType}/{from}/{to}/{language}")]
        public async Task<IActionResult> GetMessages(
            int custId,
            int messageType,
            DateTime from,
            DateTime to,
            string language)
        {
            var messages = await mediator.Send(new MessageQuery(custId, messageType, language, from, to));

            return Ok(messages);
        }
    }
}