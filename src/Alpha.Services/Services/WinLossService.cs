namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface WinLossService
    {
        Task<IEnumerable<WinLossModel>> GetWinLoss(WinLossParam param);
    }

    public class WinLossServiceImpl : WinLossService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ReportDateService reportDateService;

        public WinLossServiceImpl(
            IDynamicRepository dynamicRepository,
            ReportDateService reportDateService)
        {
            this.dynamicRepository = dynamicRepository;
            this.reportDateService = reportDateService;
        }

        public async Task<IEnumerable<WinLossModel>> GetWinLoss(WinLossParam param)
        {
            var winLossData = new List<WinLossModel>();
            var reportDate = await reportDateService.GetBalanceUpDayAndMoveBetsDay();

            var winLossDTOs = await dynamicRepository.FetchAsync<WinLossDTO>(
                new GetWinLossCriteria(
                    param.CustLevel,
                    param.CustId,
                    param.Date.GetGreaterDate(param.LimitedDate),
                    param.Date,
                    reportDate.BalanceUpDay,
                    reportDate.MoveBetsDay,
                    param.ProductIds));

            foreach (var dto in winLossDTOs)
            {
                winLossData.Add(BuildWinLoss(dto, param.CustLevel));
            }

            return winLossData;
        }

        private static WinLossModel BuildWinLoss(WinLossDTO winLossDTO, UserLevel custLevel)
            => new WinLossModel(
                custId: winLossDTO.CustId,
                custName: winLossDTO.CustName,
                grossComm: winLossDTO.GrossComm,
                turnover: winLossDTO.TurnOver,
                memberNetWinLoss: BuildMemberNetWinLoss(winLossDTO, custLevel),
                agentNetWinLoss: BuildAgentNetWinLoss(winLossDTO, custLevel),
                masterNetWinLoss: BuildMasterNetWinLoss(winLossDTO, custLevel),
                superNetWinLoss: BuildSuperNetWinLoss(winLossDTO, custLevel));

        private static NetWinLoss BuildMemberNetWinLoss(WinLossDTO winLossDTO, UserLevel custLevel)
            => custLevel.IsAgentLevel()
            ? new NetWinLoss(winLossDTO.MemberWinLoss, winLossDTO.MemberComm)
            : null;

        private static NetWinLoss BuildAgentNetWinLoss(WinLossDTO winLossDTO, UserLevel custLevel)
            => custLevel.IsNotSuperLevel()
            ? (custLevel.IsMasterLevel()
                ? new NetWinLoss(winLossDTO.AgentTotal)
                : new NetWinLoss(winLossDTO.AgentWinLoss, winLossDTO.AgentComm))
            : null;

        private static NetWinLoss BuildMasterNetWinLoss(WinLossDTO winLossDTO, UserLevel custLevel)
            => custLevel.IsMasterOrSuperLevel()
            ? (custLevel.IsMasterLevel()
                ? new NetWinLoss(winLossDTO.MasterWinLoss, winLossDTO.MasterComm)
                : new NetWinLoss(winLossDTO.MasterTotal))
            : null;

        private static NetWinLoss BuildSuperNetWinLoss(WinLossDTO winLossDTO, UserLevel custLevel)
            => custLevel.IsSuperLevel() ?
            new NetWinLoss(winLossDTO.SuperWinLoss, winLossDTO.SuperComm) :
            null;
    }
}