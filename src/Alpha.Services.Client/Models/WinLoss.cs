namespace Alpha.Services.Client.Models
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class WinLoss
    {
        public decimal TurnOver { get; set; }

        public decimal GrossComm { get; set; }

        public NetWinLoss MemberWinLoss { get; set; }

        public NetWinLoss AgentWinLoss { get; set; }

        public NetWinLoss MasterWinLoss { get; set; }

        public NetWinLoss SuperWinLoss { get; set; }
    }
}