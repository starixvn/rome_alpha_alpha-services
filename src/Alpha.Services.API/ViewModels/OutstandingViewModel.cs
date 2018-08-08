namespace Alpha.Services.API.ViewModels
{
    public class OutstandingViewModel
    {
        public OutstandingViewModel(
            byte level,
            decimal outstanding,
            decimal totalOutstanding)
        {
            Level = level;
            Outstanding = outstanding;
            TotalOutstanding = totalOutstanding;
        }

        public byte Level { get; }

        public decimal Outstanding { get; }

        public decimal TotalOutstanding { get; }
    }
}