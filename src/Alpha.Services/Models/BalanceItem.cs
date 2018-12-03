namespace Alpha.Services.Models
{
    public class BalanceItem
    {
        public BalanceItem()
        {
        }

        public BalanceItem(decimal balance, decimal yesterdayBalance)
        {
            Balance = balance;
            YesterdayBalance = yesterdayBalance;
        }

        public decimal Balance { get; private set; }

        public decimal YesterdayBalance { get; private set; }

        public static BalanceItem operator +(BalanceItem item1, BalanceItem item2)
            => new BalanceItem(
                item1.Balance + item2.Balance,
                item1.YesterdayBalance + item2.YesterdayBalance);
    }
}
