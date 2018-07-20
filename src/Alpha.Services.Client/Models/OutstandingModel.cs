namespace Alpha.Services.Client.Models
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class OutstandingModel
    {
        public decimal Outstanding { get; set; }

        public decimal TotalOutstanding { get; set; }
    }
}