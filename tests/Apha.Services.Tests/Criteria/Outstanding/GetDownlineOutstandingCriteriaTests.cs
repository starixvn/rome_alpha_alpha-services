namespace Alpha.Services.Tests.Criteria.Outstanding
{
    using Alpha.Services.Criteria.Outstanding;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using System.Linq;
    using Xunit;

    public class GetDownlineOutstandingCriteriaTests
    {
        [Theory]
        [InlineData(2, "Age_Rpt_DownlineOutstanding_Member")]
        [InlineData(3, "Age_Rpt_DownlineOutstanding_Agent")]
        [InlineData(4, "Age_Rpt_DownlineOutstanding_Master")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetDownlineOutstandingCriteria(custLevel, 0, Enumerable.Empty<int>(), 1);

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
            var criteria = new GetDownlineOutstandingCriteria(UserLevel.Agent, custId, Enumerable.Empty<int>(), 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_CustLevelIsMember_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetDownlineOutstandingCriteria(UserLevel.Member, 1, Enumerable.Empty<int>(), 1);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsValid_InvalidDownlineId_ReturnsFalse(int downlineId)
        {
            // Arrange
            var criteria = new GetDownlineOutstandingCriteria(UserLevel.Agent, 1, Enumerable.Empty<int>(), downlineId);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        public void IsValid_ValidCriteria_ReturnsTrue(byte custLevel)
        {
            // Arrange
            var criteria = new GetDownlineOutstandingCriteria(
                Enumeration.FromValue<UserLevel>(custLevel),
                1,
                Enumerable.Empty<int>(),
                1);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}
