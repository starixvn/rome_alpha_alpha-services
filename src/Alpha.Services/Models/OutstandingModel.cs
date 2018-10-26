namespace Alpha.Services.Models
{
    public class OutstandingModel
    {
        public OutstandingModel()
        {
        }

        public OutstandingModel(
            int custId,
            string custName,
            decimal memberOutstanding,
            decimal outstanding)
        {
            CustId = custId;
            CustName = custName;
            MemberOutstanding = memberOutstanding;
            Outstanding = outstanding;
        }

        public int CustId { get; }

        public string CustName { get; }

        public decimal MemberOutstanding { get; private set; }

        public decimal Outstanding { get; private set; }

        public OutstandingModel Add(OutstandingModel outstanding)
        {
            if (outstanding == null)
            {
                return this;
            }

            MemberOutstanding += outstanding.MemberOutstanding;
            Outstanding += outstanding.Outstanding;

            return this;
        }
    }
}