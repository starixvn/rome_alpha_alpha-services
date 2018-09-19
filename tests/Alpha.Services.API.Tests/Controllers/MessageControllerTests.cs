namespace Alpha.Services.API.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
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
            var fromTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var toTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var customers = new List<CustomerInfo>
            {
                new CustomerInfo
                {
                    CustId=1,
                    RoleId=1,
                    Language="en-US"
                }
            };

            var query = new MessageQuery
            {
                MessageType = 1,
                FromTime = fromTime,
                ToTime = toTime,
                Customers = customers
            };

            // Act
            messageController.GetMessages(query).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Any<MessageQuery>());
        }
    }
}