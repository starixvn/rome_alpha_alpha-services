namespace Alpha.Services.API.ViewModels
{
    public class OutstandingViewModel
    {
        public OutstandingViewModel(
            byte levelId,
            decimal outstanding,
            decimal totalOutstanding)
        {
            LevelId = levelId;
            Outstanding = outstanding;
            TotalOutstanding = totalOutstanding;
        }

        public byte LevelId { get; }

        public decimal Outstanding { get; }

        public decimal TotalOutstanding { get; }
    }
}