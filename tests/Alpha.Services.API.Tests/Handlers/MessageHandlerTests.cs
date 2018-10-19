namespace Alpha.Services.API.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Configurations;
    using Alpha.Services.Constants;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class MessageHandlerTests
    {
        private readonly IAppSettings appSettings;
        private readonly MessageService messageService;
        private readonly CustomerService customerService;
        private readonly MessageHandler messageHandler;

        public MessageHandlerTests()
        {
            appSettings = Substitute.For<IAppSettings>();
            messageService = Substitute.For<MessageService>();
            customerService = Substitute.For<CustomerService>();

            messageHandler = new MessageHandler(appSettings, messageService, customerService);

            appSettings.IsLocalTest.Returns(true);
        }

        [Fact]
        public void Handle_Always_CallsMessageServiceGetMessages()
        {
            // Arrange
            var fromTime = DateTime.Now;
            var toTime = DateTime.Now;
            var request = CreateMessageQuery(1, fromTime, toTime);
            messageService.GetMessages(Arg.Any<MessageParam>())
                .Returns(Task.FromResult(Enumerable.Empty<Message>()));

            // Act
            messageHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            messageService.Received().GetMessages(Arg.Is<MessageParam>(
                p => p.MessageType == request.MessageType
                    && p.FromTime.ToString() == fromTime.ToString()
                    && p.ToTime.ToString() == toTime.ToString()
                    && p.IsLocalTest == appSettings.IsLocalTest));
        }

        [Fact]
        public void Handle_MessageTypeIsPersonal_DoesNotCallGetCustomerParentSites()
        {
            // Arrange
            var fromTime = DateTime.Now;
            var toTime = DateTime.Now;
            var request = CreateMessageQuery(MessageTypeConstants.Personal, fromTime, toTime);

            IEnumerable<Message> messages = new List<Message>
            {
                new Message(
                    id: 1,
                    messageType: MessageTypeConstants.Personal,
                    contentEN: "Content",
                    contentCS: string.Empty,
                    contentCH: string.Empty,
                    contentJP: string.Empty,
                    contentTH: string.Empty,
                    contentKO: string.Empty,
                    contentVI: string.Empty,
                    targetId: 1,
                    betTransId: string.Empty)
            };
            messageService.GetMessages(Arg.Any<MessageParam>())
                .Returns(Task.FromResult(messages));

            // Act
            messageHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.DidNotReceive().GetCustomerInfo(Arg.Any<int>());
            appSettings.DidNotReceive().GetParentSites(Arg.Any<string>(), Arg.Any<string>());
        }

        [Theory]
        [InlineData(MessageTypeConstants.Special)]
        [InlineData(MessageTypeConstants.Normal)]
        public void Handle_MessageTypeIsNotPersonal_CallsGetCustomerParentSites(byte messageType)
        {
            // Arrange
            var fromTime = DateTime.Now;
            var toTime = DateTime.Now;
            var request = CreateMessageQuery(MessageTypeConstants.Personal, fromTime, toTime);

            IEnumerable<Message> messages = new List<Message>
            {
                new Message(
                    id: 1,
                    messageType: messageType,
                    contentEN: "Content",
                    contentCS: string.Empty,
                    contentCH: string.Empty,
                    contentJP: string.Empty,
                    contentTH: string.Empty,
                    contentKO: string.Empty,
                    contentVI: string.Empty,
                    targetId: 1,
                    betTransId: string.Empty)
            };
            messageService.GetMessages(Arg.Any<MessageParam>())
                .Returns(Task.FromResult(messages));

            var customer = new Customer(1, "username", CustLevel.Agent, "UUS", 1);
            customerService.GetCustomerInfo(Arg.Any<int>())
                .Returns(Task.FromResult(customer));

            // Act
            messageHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().GetCustomerInfo(Arg.Any<int>());
            appSettings.Received().GetParentSites(Arg.Is(customer.CustLevel.DisplayName), Arg.Is(customer.System));
        }

        private MessageQuery CreateMessageQuery(int messageType, DateTime fromTime, DateTime toTime)
            => new MessageQuery
            {
                MessageType = messageType,
                FromTime = new DateTimeOffset(fromTime).ToUnixTimeMilliseconds(),
                ToTime = new DateTimeOffset(toTime).ToUnixTimeMilliseconds(),
                Customers = new List<CustomerInfo>
                {
                    new CustomerInfo { CustId = 1, RoleId = CustLevel.Agent, Language = "en-US" }
                }
            };
    }
}