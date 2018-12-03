namespace Alpha.Services.Client.Models
{
    using System.Collections.Generic;

    public class BalanceTotal
    {
        public byte CustLevel { get; set; }

        public int HiddenDownlinesCount { get; set; }

        public BalanceItem Total { get; set; }

        public IEnumerable<BalanceItem> Balances { get; set; }
    }
}
