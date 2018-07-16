namespace Alpha.Services.Params
{
    using Alpha.Services.Models;

    public class BalanceInfoParam
    {
        public BalanceInfoParam(
            UserLevel custLevel,
            int custId)
        {
            CustLevel = custLevel;
            CustId = custId;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }
    }
}