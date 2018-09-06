namespace Apha.Services.Tests.Criteria
{
    using System;
    using Alpha.Services.Constants;
    using Alpha.Services.Criteria;
    using Xunit;

    public class GetMessagesCriteriaTests
    {
        [Theory]
        [InlineData(MessageTypeConstants.Personal, "Dot_Msg_GetPrivateMessage")]
        [InlineData(MessageTypeConstants.Normal, "Age_GetNormalMessage")]
        [InlineData(MessageTypeConstants.Special, "Dot_Msg_GetSpecialMessage")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(int messageType, string expectedSettingKey)
        {
            // Arrange
            var criteria = new GetMessagesCriteria(0, string.Empty, 0, DateTime.Now, DateTime.Now, true, messageType);

            // Act & Assert
            Assert.Equal(expectedSettingKey, criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetMessagesCriteria(custId, "3000,3300", 0, DateTime.Now, DateTime.Now, true, 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void IsValid_ParentSitesIsNullOrWhiteSpace_ReturnsFalse(string parentSites)
        {
            // Arrange
            var criteria = new GetMessagesCriteria(1, parentSites, 0, DateTime.Now, DateTime.Now, true, 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_EndDateIsLessThanFromDate_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetMessagesCriteria(1, "3000,3300", 0, DateTime.Now, DateTime.Now.AddDays(-1), true, 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetMessagesCriteria(1, "3000,3300", 0, DateTime.Now, DateTime.Now, true, 1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}