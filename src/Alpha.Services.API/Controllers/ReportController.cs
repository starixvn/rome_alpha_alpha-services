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
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{custId}/outstanding")]
        public async Task<IActionResult> GetOutstanding(int custId = 1176086)
        {
            var outstanding = await mediator.Send(new OutstandingQuery(custId));

            return Ok(outstanding);
        }

        [HttpGet("{custId}/balanceinfo")]
        public async Task<IActionResult> GetBalanceInfo(int custId = 1176086)
        {
            var balanceInfo = await mediator.Send(new BalanceInfoQuery(custId));

            return Ok(balanceInfo);
        }

        [HttpGet("{custId}/winloss/{date}")]
        public async Task<IActionResult> GetWinLoss(int custId = 1176086, long date = 1532922530926)
        {
            var winLoss = await mediator.Send(
                new WinLossQuery(custId, DateTimeOffset.FromUnixTimeMilliseconds(date).DateTime.ToLocalTime()));

            return Ok(winLoss);
        }
    }
}