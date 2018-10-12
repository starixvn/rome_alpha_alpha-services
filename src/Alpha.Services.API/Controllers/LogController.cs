namespace Alpha.Services.API.Controllers
{
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [FormatFilter]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "HMAC")]
    public class LogController : ControllerBase
    {
        private readonly IMediator mediator;

        public LogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLog([FromBody]InsertLogCommand command)
        {
            var commandResult = await mediator.Send(command);

            return commandResult
                ? Ok()
                : BadRequest() as IActionResult;
        }
    }
}