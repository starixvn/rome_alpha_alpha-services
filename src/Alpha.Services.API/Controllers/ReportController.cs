namespace Alpha.Services.API.Controllers
{
    using Alpha.Services.API.Requests;
    using Alpha.Services.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [FormatFilter]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "HMAC")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
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
            var winLoss = await mediator.Send(new WinLossQuery(custId, custLevel, date.ToLocalTime()));

            return Ok(winLoss);
        }

        [HttpGet("{custId}/{custLevel}/outstandings/{top}")]
        public async Task<IActionResult> GetOutstandings(int custId, byte custLevel, int top)
            => Ok(await mediator.Send(new OutstandingQuery(custId, custLevel, top)));

        [HttpGet("{custId}/{custLevel}/outstanding/{downlineId}")]
        public async Task<IActionResult> GetDownlineOutstanding(int custId, byte custLevel, int downlineId)
            => Ok(await mediator.Send(new DownlineOutstandingQuery(custId, custLevel, downlineId)));
    }
}