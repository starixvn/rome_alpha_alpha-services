namespace Alpha.Services.Models
{
    public class WinLossModel
    {
        public WinLossModel()
        {
        }

        public WinLossModel(
            decimal turnover,
            decimal grossComm,
            decimal memberWinLoss,
            decimal memberComm,
            decimal agentWinLoss,
            decimal agentComm,
            decimal agentTotal,
            decimal masterWinLoss,
            decimal masterComm,
            decimal masterTotal,
            decimal superWinLoss,
            decimal superComm)
        {
            Turnover = turnover;
            GrossComm = grossComm;
            MemberWinLoss = memberWinLoss;
            MemberComm = memberComm;
            AgentWinLoss = agentWinLoss;
            AgentComm = agentComm;
            AgentTotal = agentTotal;
            MasterWinLoss = masterWinLoss;
            MasterComm = masterComm;
            MasterTotal = masterTotal;
            SuperWinLoss = superWinLoss;
            SuperComm = superComm;
        }

        public decimal Turnover { get; }

        public decimal GrossComm { get; }

        public decimal MemberWinLoss { get; }

        public decimal MemberComm { get; }

        public decimal MemberTotal => MemberWinLoss + MemberComm;

        public decimal AgentWinLoss { get; }

        public decimal AgentComm { get; }

        public decimal AgentTotal { get; }

        public decimal MasterWinLoss { get; }

        public decimal MasterComm { get; }

        public decimal MasterTotal { get; }

        public decimal SuperWinLoss { get; }

        public decimal SuperComm { get; }

        public decimal SuperTotal => SuperWinLoss + SuperComm;
    }
}