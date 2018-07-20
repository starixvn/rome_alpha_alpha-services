namespace Alpha.Services.Client.Models
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class NetWinLoss
    {
        public decimal? WinLoss { get; set; }

        public decimal? Comm { get; set; }

        public decimal Total { get; set; }
    }
}