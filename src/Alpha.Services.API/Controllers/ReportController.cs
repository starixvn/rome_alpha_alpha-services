namespace Alpha.Services.API.Controllers
{
    using System;
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

        [HttpGet("{custId}/outstanding")]
        public async Task<IActionResult> GetOutstanding(int custId)
        {
            var outstanding = await mediator.Send(new OutstandingQuery(custId));

            return Ok(outstanding);
        }

        [HttpGet("{custId}/balanceinfo")]
        public async Task<IActionResult> GetBalanceInfo(int custId)
        {
            var balanceInfo = await mediator.Send(new BalanceInfoQuery(custId));

            return Ok(balanceInfo);
        }

        [HttpGet("{custId}/winloss/{date}")]
        public async Task<IActionResult> GetWinLoss(int custId, DateTime date)
        {
            var winLoss = await mediator.Send(new WinLossQuery(custId, date));

            return Ok(winLoss);
        }
    }
}