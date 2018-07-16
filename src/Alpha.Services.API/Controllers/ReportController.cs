namespace Alpha.Services.API.Controllers
{
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOutstanding(int custId)
        {
            var outstanding = await mediator.Send(new OutstandingQuery(custId));

            return Ok(outstanding);
        }
    }
}