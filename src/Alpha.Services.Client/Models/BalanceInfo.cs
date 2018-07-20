namespace Alpha.Services.Client.Models
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class BalanceInfo
    {
        public string Currency { get; set; }

        public decimal CashBalance { get; set; }

        public decimal YesterdayCashBalance { get; set; }

        public decimal TotalBalance { get; set; }

        public decimal YesterdayTotalBalance { get; set; }

        public decimal TodayWinLoss { get; set; }

        public decimal YesterdayWinLoss { get; set; }

        public decimal GivenCredit { get; set; }

        public decimal MemberCredit { get; set; }
    }
}