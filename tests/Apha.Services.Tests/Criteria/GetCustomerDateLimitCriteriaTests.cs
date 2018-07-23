namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetCustomerDateLimitCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnValidSettingKey()
        {
            // Arrange
            var criteria = new GetCustomerDateLimitCriteria(0);

            // Act & Assert
            Assert.Equal("Age_CustomerSelDateLimit", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetCustomerDateLimitCriteria(custId);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetCustomerDateLimitCriteria(1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}