namespace Alpha.Services.API.ViewModels
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class OutstandingViewModel
    {
        public OutstandingViewModel(decimal outstanding, decimal totalOutstanding)
        {
            Outstanding = outstanding;
            TotalOutstanding = totalOutstanding;
        }

        public decimal Outstanding { get; }

        public decimal TotalOutstanding { get; }
    }
}