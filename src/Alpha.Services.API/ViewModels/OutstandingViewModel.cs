namespace Alpha.Services.API.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Services.Models;

    public class OutstandingViewModel
    {
        public OutstandingViewModel(
            byte custLevel,
            int hiddenDownlinesCount,
            OutstandingModel total,
            IEnumerable<OutstandingModel> topOutstandings,
            string downlineName = "")
        {
            CustLevel = custLevel;
            HiddenDownlinesCount = hiddenDownlinesCount > 0 ? hiddenDownlinesCount : 0;
            Total = total;
            TopOutstandings = topOutstandings;
            DownlineName = downlineName;
        }

        public byte CustLevel { get; }

        public int HiddenDownlinesCount { get; }

        public OutstandingModel Total { get; }

        public IEnumerable<OutstandingModel> TopOutstandings { get; }

        public string DownlineName { get; }
    }
}