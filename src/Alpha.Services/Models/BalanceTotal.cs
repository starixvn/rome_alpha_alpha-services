namespace Alpha.Services.Models
{
    using System.Collections.Generic;

    public class BalanceTotal
    {
        public BalanceTotal(
            byte custLevel,
            int hiddenDownlinesCount,
            BalanceItem total,
            IEnumerable<BalanceItem> balances)
        {
            CustLevel = custLevel;
            HiddenDownlinesCount = hiddenDownlinesCount;
            Total = total;
            Balances = balances;
        }

        public byte CustLevel { get; }

        public int HiddenDownlinesCount { get; }

        public BalanceItem Total { get; }

        public IEnumerable<BalanceItem> Balances { get; }
    }
}
