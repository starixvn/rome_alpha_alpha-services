namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetCustomerStatusCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var criteria = new GetCustomerStatusCommand(1, 0);

            // Act & Assert
            Assert.Equal("Age_CustomerStatusSel", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetCustomerStatusCommand(custId, 0);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCustId_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetCustomerStatusCommand(1, 0);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}