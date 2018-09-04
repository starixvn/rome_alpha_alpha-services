namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetCustomerInfoCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnValidSettingKey()
        {
            // Arrange
            var criteria = new GetCustomerInfoCriteria(0);

            // Act & Assert
            Assert.Equal("Chatbot_GetCustomerInfo", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetCustomerInfoCriteria(custId);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetCustomerInfoCriteria(1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}