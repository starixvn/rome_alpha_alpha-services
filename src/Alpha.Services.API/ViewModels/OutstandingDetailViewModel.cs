namespace Alpha.Services.API.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Services.Models;

    public class OutstandingDetailViewModel
    {
        public OutstandingDetailViewModel(
            byte custLevel,
            int hiddenDownlinesCount,
            OutstandingDetail total,
            IEnumerable<OutstandingDetail> topOutstandings)
        {
            CustLevel = custLevel;
            HiddenDownlinesCount = hiddenDownlinesCount > 0 ? hiddenDownlinesCount : 0;
            Total = total;
            TopOutstandings = topOutstandings;
        }

        public byte CustLevel { get; }

        public int HiddenDownlinesCount { get; }

        public OutstandingDetail Total { get; }

        public IEnumerable<OutstandingDetail> TopOutstandings { get; }
    }
}