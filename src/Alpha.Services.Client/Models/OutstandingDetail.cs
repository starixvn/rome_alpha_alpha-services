namespace Alpha.Services.Client.Models
{
    using System.Collections.Generic;

    public class OutstandingDetail
    {
        public string CustLevel { get; set; }

        public OutstandingModel Total { get; set; }

        public IEnumerable<OutstandingModel> Outstandings { get; set; }
    }
}