namespace Apha.Services.Tests.Criteria
{
    using Alpha.Services.Criteria;
    using Xunit;

    public class InsertUserLogCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectKey()
        {
            // Arrange
            var command = new InsertUserLogCommand(1, 1, 1, string.Empty, string.Empty, 0);

            // Act & Assert
            Assert.Equal("Acc_WriteLog_UserLog_RunSP", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidActorId_ReturnsFalse(int actorId)
        {
            // Arrange
            var command = new InsertUserLogCommand(actorId, 1, 1, "trackingvalue", string.Empty, 0);

            // Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var command = new InsertUserLogCommand(1, custId, 1, "trackingvalue", string.Empty, 0);

            // Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidActionId_ReturnsFalse(int actionId)
        {
            // Arrange
            var command = new InsertUserLogCommand(1, 1, actionId, "trackingvalue", string.Empty, 0);

            // Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void IsValid_InvalidTrackingValue_ReturnsFalse(string trackingValue)
        {
            // Arrange
            var command = new InsertUserLogCommand(1, 1, 1, trackingValue, string.Empty, 0);

            // Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var command = new InsertUserLogCommand(1, 1, 1, "trackingValue", string.Empty, 0);

            // Act & Assert
            Assert.True(command.IsValid());
        }
    }
}