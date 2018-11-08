namespace Alpha.Services.Models
{
    public class OutstandingModel
    {
        public OutstandingModel()
        {
        }

        public OutstandingModel(
            int id,
            string name,
            decimal memberOutstanding,
            decimal outstanding)
        {
            Id = id;
            Name = name;
            MemberOutstanding = memberOutstanding;
            Outstanding = outstanding;
        }

        // CustId or ProductId.
        public int Id { get; }

        // CustName or ProductName from RemotingRef.
        public string Name { get; set; }

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