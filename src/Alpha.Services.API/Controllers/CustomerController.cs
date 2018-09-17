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
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/isaccountclosed/{subAccountId}")]
        public async Task<ActionResult> IsAccountClosed(int custId, int subAccountId)
        {
            var isClosed = await mediator.Send(new GetAccountStatusQuery(custId, subAccountId));

            return Ok(isClosed);
        }
    }
}