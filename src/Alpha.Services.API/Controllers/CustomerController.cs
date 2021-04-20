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

        [HttpGet("{custId}")]
        public async Task<ActionResult> GetCustomer(int custId)
        {
            var customer = await mediator.Send(new GetCustomerQuery(custId));

            return Ok(customer);
        }

        [HttpGet("{custId}/isaccountclosed/{subAccountId}")]
        public async Task<ActionResult> IsAccountClosed(int custId, int subAccountId)
        {
            var accountStatus = await mediator.Send(new GetAccountStatusQuery(custId, subAccountId));

            return Ok(!accountStatus.IsActive);
        }

        [HttpGet("{custId}/getaccountstatus/{subAccountId}")]
        public async Task<ActionResult> GetAccountStatus(int custId, int subAccountId)
        {
            var accountStatus = await mediator.Send(new GetAccountStatusQuery(custId, subAccountId));

            return Ok(accountStatus);
        }

        [HttpGet("{custId}/downline")]
        public async Task<ActionResult> GetDownlineInfo(int custId, string downlineUserName)
        {
            var downlineInfo = await mediator.Send(new DownlineInfoQuery(custId, downlineUserName));

            return Ok(downlineInfo);
        }
    }
}