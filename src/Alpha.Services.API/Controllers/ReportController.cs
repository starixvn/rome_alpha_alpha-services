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
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/{custLevel}/outstanding")]
        public async Task<IActionResult> GetOutstanding(int custId, byte custLevel)
        {
            var outstanding = await mediator.Send(new OutstandingQuery(custId, custLevel));

            return Ok(outstanding);
        }

        [HttpGet("{custId}/balanceinfo")]
        public async Task<IActionResult> GetBalanceInfo(int custId)
        {
            var balanceInfo = await mediator.Send(new BalanceInfoQuery(custId));

            return Ok(balanceInfo);
        }

        [HttpGet("{custId}/{custLevel}/winloss/{date}")]
        public async Task<IActionResult> GetWinLoss(int custId, byte custLevel, long date)
        {
            var winLoss = await mediator.Send(
                new WinLossQuery(custId, custLevel, date.ToLocalTime()));

            return Ok(winLoss);
        }
    }
}