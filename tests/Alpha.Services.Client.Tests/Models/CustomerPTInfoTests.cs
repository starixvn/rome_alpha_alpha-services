namespace Alpha.Services.Client.Tests.Models
{
    using Alpha.Services.Client.Models;
    using Xunit;

    public class CustomerPTInfoTests
    {
        [Fact]
        public void CustomerPTInfo_DefaultConstructor_AssignsCorrectDefaultValue()
        {
            // Arrange & Act
            var ptInfo = new CustomerPTInfo();

            // Assert
            Assert.True(
                ptInfo.CustId == 0
                && ptInfo.CustLevel == 0
                && ptInfo.Suffix == null
                && ptInfo.MaxCredit == 0);
        }

        [Fact]
        public void CustomerPTInfo_Always_AssignsCorrectValue()
        {
            // Arrange & Act
            var ptInfo = new CustomerPTInfo
            {
                CustId = 8,
                CustLevel = 4,
                Suffix = "00",
                MaxCredit = 1000
            };

            // Assert
            Assert.True(
                ptInfo.CustId == 8
                && ptInfo.CustLevel == 4
                && ptInfo.Suffix == "00"
                && ptInfo.MaxCredit == 1000);
        }
    }
}
