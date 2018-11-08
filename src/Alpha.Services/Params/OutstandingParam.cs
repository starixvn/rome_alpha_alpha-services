namespace Alpha.Services.Params
{
    using Alpha.Services.Models;
    using System.Collections.Generic;

    public class OutstandingParam
    {
        public OutstandingParam(
            UserLevel custLevel,
            int custId,
            IEnumerable<int> productIds,
            string language = "en-US")
        {
            CustLevel = custLevel;
            CustId = custId;
            ProductIds = productIds;
            Language = language;
        }

        public UserLevel CustLevel { get; }

        public int CustId { get; }

        public IEnumerable<int> ProductIds { get; }

        public string Language { get; }
    }
}