namespace Alpha.Services.API.ViewModels
{
    using Alpha.Services.Models;

    public class WinLossViewModel
    {
        public WinLossViewModel(
            decimal turnOver,
            decimal grossComm,
            NetWinLoss memberWinLoss,
            NetWinLoss agentWinLoss,
            NetWinLoss masterWinLoss,
            NetWinLoss superWinLoss)
        {
            TurnOver = turnOver;
            GrossComm = grossComm;
            MemberWinLoss = memberWinLoss;
            AgentWinLoss = agentWinLoss;
            MasterWinLoss = masterWinLoss;
            SuperWinLoss = superWinLoss;
        }

        public decimal TurnOver { get; }

        public decimal GrossComm { get; }

        public NetWinLoss MemberWinLoss { get; }

        public NetWinLoss AgentWinLoss { get; }

        public NetWinLoss MasterWinLoss { get; }

        public NetWinLoss SuperWinLoss { get; }
    }
}