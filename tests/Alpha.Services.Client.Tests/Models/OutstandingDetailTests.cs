namespace Alpha.Services.Client.Tests.Models
{
    using System.Linq;
    using Alpha.Services.Client.Models;
    using Xunit;

    public class OutstandingDetailTests
    {
        [Fact]
        public void OutstandingDetail_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new OutstandingDetail
            {
                CustLevel = 2,
                HiddenDownlinesCount = 10,
                Total = null,
                TopOutstandings = Enumerable.Empty<OutstandingModel>(),
                DownlineName = "member1"
            };

            // Assert
            Assert.True(
                2 == model.CustLevel
                && 10 == model.HiddenDownlinesCount
                && null == model.Total
                && false == model.TopOutstandings.Any()
                && "member1" == model.DownlineName);
        }
    }
}
