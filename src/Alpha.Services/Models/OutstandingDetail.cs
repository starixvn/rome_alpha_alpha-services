namespace Alpha.Services.Models
{
    public class OutstandingDetail
    {
        public OutstandingDetail()
        {
        }

        public OutstandingDetail(
            int custId,
            string custName,
            decimal memberOutstanding,
            decimal agentOutstanding,
            decimal masterOutstanding,
            decimal superOutstanding)
        {
            CustId = custId;
            CustName = custName;
            MemberOutstanding = memberOutstanding;
            AgentOutstanding = agentOutstanding;
            MasterOutstanding = masterOutstanding;
            SuperOutstanding = superOutstanding;
        }

        public int CustId { get; }

        public string CustName { get; }

        public decimal MemberOutstanding { get; }

        public decimal AgentOutstanding { get; }

        public decimal MasterOutstanding { get; }

        public decimal SuperOutstanding { get; }
    }
}