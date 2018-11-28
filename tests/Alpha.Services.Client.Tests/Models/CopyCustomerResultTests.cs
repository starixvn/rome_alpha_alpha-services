namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class CopyCustomerResultTests
    {
        [Fact]
        public void CopyCustomerResult_DefaultConstructor_AssignsCorrectDefaultValue()
        {
            // Arrange & Act
            var copyResult = new CopyCustomerResult();

            // Assert
            Assert.True(
                copyResult.CustId == 0
                && copyResult.CustName == null
                && copyResult.Password == null
                && copyResult.Code == 0
                && copyResult.Message == null
                && !copyResult.IsSuccess);
        }

        [Fact]
        public void CopyCustomerResult_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var copyResult = new CopyCustomerResult
            {
                CustId = 8,
                CustName = "master00",
                Password = "1234aaAA",
                Code = 0,
                Message = string.Empty,
                IsSuccess = true
            };

            // Assert
            Assert.True(
                copyResult.CustId == 8
                && copyResult.CustName == "master00"
                && copyResult.Password == "1234aaAA"
                && copyResult.Code == 0
                && copyResult.Message == string.Empty
                && copyResult.IsSuccess);
        }
    }
}
