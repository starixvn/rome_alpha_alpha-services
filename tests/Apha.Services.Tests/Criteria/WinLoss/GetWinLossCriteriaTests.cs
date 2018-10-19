namespace Alpha.Services.Tests.Criteria.WinLoss
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Services.Criteria.WinLoss;
    using Alpha.Services.Enumerations;
    using Alpha.Services.Models;
    using Xunit;

    public class GetWinLossCriteriaTests
    {
        [Theory]
        [InlineData(2, "Age_Rpt_SumWinLoss_Member")]
        [InlineData(3, "Age_Rpt_SumWinLoss_Agent")]
        [InlineData(4, "Age_Rpt_SumWinLoss_Master")]
        public void GetSettingKey_WithCorrectCustLevel_ReturnsValidSettingKey(byte custLevelId, string expectedSettingKey)
        {
            // Arrange
            var custLevel = Enumeration.FromValue<UserLevel>(custLevelId);
            var criteria = new GetWinLossCriteria(
                custLevel,
                0,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                Enumerable.Empty<int>());

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
            var criteria = new GetWinLossCriteria(
                UserLevel.Agent,
                custId,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                new List<int> { 1, 2 });

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_FromDateIsLessThanToDate_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetWinLossCriteria(
                UserLevel.Agent,
                1,
                DateTime.Now,
                DateTime.Now.AddDays(-1),
                DateTime.Now,
                DateTime.Now,
                new List<int> { 1, 2 });

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_EmptyProductList_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetWinLossCriteria(
                UserLevel.Agent,
                1,
                DateTime.Now,
                DateTime.Now.AddDays(-1),
                DateTime.Now,
                DateTime.Now,
                Enumerable.Empty<int>());

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetWinLossCriteria(
                UserLevel.Agent,
                1,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                new List<int> { 1, 2 });

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}