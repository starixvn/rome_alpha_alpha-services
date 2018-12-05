namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class ResultTests
    {
        [Fact]
        public void Result_DefaultConstructor_AssignsCorrectDefaultValue()
        {
            // Arrange & Act
            var copyResult = new Result();

            // Assert
            Assert.True(
                copyResult.Code == 0
                && copyResult.Message == null
                && !copyResult.IsSuccess);
        }

        [Fact]
        public void Result_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var copyResult = new Result
            {
                Code = 201,
                Message = "Test message",
                IsSuccess = true
            };

            // Assert
            Assert.True(
                copyResult.Code == 201
                && copyResult.Message == "Test message"
                && copyResult.IsSuccess);
        }
    }
}
