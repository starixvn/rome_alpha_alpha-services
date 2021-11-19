namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class MessageTests
    {
        [Fact]
        public void Message_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var model = new Message
            {
                CustId = 2,
                Content = "qwerty"
            };

            // Assert
            Assert.True(
                2 == model.CustId
                && "qwerty" == model.Content);
        }
    }
}
