namespace Alpha.Services.Models
{
    public class BalanceInfo
    {
        public BalanceInfo()
        {
        }

        public BalanceInfo(
            decimal cashBalance,
            decimal yesterdayCashBalance,
            decimal totalBalance,
            decimal yesterdayTotalBalance,
            decimal todayWinLoss,
            decimal yesterdayWinLoss,
            decimal givenCredit,
            decimal totalCredit)
        {
            CashBalance = cashBalance;
            YesterdayCashBalance = yesterdayCashBalance;
            TotalBalance = totalBalance;
            YesterdayTotalBalance = yesterdayTotalBalance;
            TodayWinLoss = todayWinLoss;
            YesterdayWinLoss = yesterdayWinLoss;
            GivenCredit = givenCredit;
            TotalCredit = totalCredit;
        }

        public decimal CashBalance { get; }

        public decimal YesterdayCashBalance { get; }

        public decimal TotalBalance { get; }

        public decimal YesterdayTotalBalance { get; }

        public decimal TodayWinLoss { get; }

        public decimal YesterdayWinLoss { get; }

        public decimal GivenCredit { get; }

        public decimal TotalCredit { get; }
    }
}