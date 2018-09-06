namespace Alpha.Services.API.Tests.Controllers
{
    using System;
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using MediatR;
    using NSubstitute;
    using Xunit;

    public class MessageControllerTests
    {
        private readonly IMediator mediator;
        private readonly MessageController messageController;

        public MessageControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            messageController = new MessageController(mediator);
        }

        [Fact]
        public void GetMessages_Always_CallsMediatorSendMessageQuery()
        {
            // Arrange
            var custId = 1;
            var messageType = 1;
            var language = "en-US";
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;

            // Act
            messageController.GetMessages(
                custId,
                CustLevel.Agent,
                messageType,
                language,
                fromTime.ToUnixTimeMilliseconds(),
                toTime.ToUnixTimeMilliseconds())
                .ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Any<MessageQuery>());
        }
    }
}