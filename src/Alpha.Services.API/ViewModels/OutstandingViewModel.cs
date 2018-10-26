namespace Alpha.Services.API.ViewModels
{
    using System.Collections.Generic;
    using Alpha.Services.Models;

    public class OutstandingViewModel
    {
        public OutstandingViewModel(
            string custLevel,
            int hiddenDownlinesCount,
            OutstandingModel total,
            IEnumerable<OutstandingModel> topOutstandings)
        {
            CustLevel = custLevel;
            HiddenDownlinesCount = hiddenDownlinesCount > 0 ? hiddenDownlinesCount : 0;
            Total = total;
            TopOutstandings = topOutstandings;
        }

        public string CustLevel { get; }

        public int HiddenDownlinesCount { get; }

        public OutstandingModel Total { get; }

        public IEnumerable<OutstandingModel> TopOutstandings { get; }
    }
}