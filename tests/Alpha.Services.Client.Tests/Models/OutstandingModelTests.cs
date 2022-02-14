namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class OutstandingModelTests
    {
        [Fact]
        public void OutstandingModel_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new OutstandingModel
            {
                Name = "super1",
                MemberOutstanding = 123,
                Outstanding = 456
            };

            // Assert
            Assert.True(
                "super1" == model.Name
                && 123 == model.MemberOutstanding
                && 456 == model.Outstanding);
        }
    }
}
