using System.Linq;
using Alpha.Services.Criteria.OutstandingDetail;
using Alpha.Services.Enumerations;
using Alpha.Services.Models;
using Xunit;

namespace Alpha.Services.Tests.Criteria.OutstandingDetail
{
    public class GetOutstandingDetailCriteriaTests
    {
        [Theory]
        [InlineData(2, "Age_Rpt_Outstanding_Member")]
        [InlineData(3, "Age_Rpt_Outstanding_Agent")]
        [InlineData(4, "Age_Rpt_Outstanding_Master")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetOutstandingDetailCriteria(custLevel, 0, Enumerable.Empty<int>());

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
            var criteria = new GetOutstandingDetailCriteria(UserLevel.Agent, custId, Enumerable.Empty<int>());

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_CustLevelIsMember_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetOutstandingDetailCriteria(UserLevel.Member, 1, Enumerable.Empty<int>());

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
            var criteria = new GetOutstandingDetailCriteria(
                custLevel: Enumeration.FromValue<UserLevel>(custLevel),
                custId: 1,
                productIds: Enumerable.Empty<int>());

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}