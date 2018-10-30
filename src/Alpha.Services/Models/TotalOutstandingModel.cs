namespace Alpha.Services.Models
{
    public class TotalOutstandingModel
    {
        public TotalOutstandingModel(decimal outstanding, decimal totalOutstanding)
        {
            Outstanding = outstanding;
            TotalOutstanding = totalOutstanding;
        }

        public decimal Outstanding { get; private set; }

        public decimal TotalOutstanding { get; private set; }
    }
}
