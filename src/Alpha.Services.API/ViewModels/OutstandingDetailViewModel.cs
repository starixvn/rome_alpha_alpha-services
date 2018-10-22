namespace Alpha.Services.API.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Services.Models;

    public class OutstandingDetailViewModel
    {
        public OutstandingDetailViewModel(byte custLevel, IEnumerable<OutstandingDetail> outstandings)
        {
            CustLevel = custLevel;
            Outstandings = outstandings;
        }

        public byte CustLevel { get; }

        public IEnumerable<OutstandingDetail> Outstandings { get; }

        public OutstandingDetail Total => AccummulateTotal(Outstandings);

        internal static OutstandingDetail AccummulateTotal(IEnumerable<OutstandingDetail> outstandingDetails)
        {
            var total = new OutstandingDetail();

            if (outstandingDetails != null && outstandingDetails.Any())
            {
                foreach (var outstanding in outstandingDetails)
                {
                    total.Add(outstanding);
                }
            }

            return total;
        }
    }
}