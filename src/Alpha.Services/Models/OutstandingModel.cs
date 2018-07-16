namespace Alpha.Services.Models
{
    public class OutstandingModel
    {
        public OutstandingModel()
        {
        }

        public OutstandingModel(decimal outstanding, decimal totalOutstanding)
        {
            Outstanding = outstanding;
            TotalOutstanding = totalOutstanding;
        }

        public decimal Outstanding { get; private set; }

        public decimal TotalOutstanding { get; private set; }
    }
}