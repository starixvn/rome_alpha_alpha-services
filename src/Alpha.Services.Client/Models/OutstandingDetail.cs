namespace Alpha.Services.Client.Models
{
    using System.Collections.Generic;

    public class OutstandingDetail
    {
        public byte CustLevel { get; set; }

        public int HiddenDownlinesCount { get; set; }

        public OutstandingModel Total { get; set; }

        public IEnumerable<OutstandingModel> TopOutstandings { get; set; }
    }
}