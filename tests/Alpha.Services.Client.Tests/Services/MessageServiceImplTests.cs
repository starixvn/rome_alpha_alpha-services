namespace Alpha.Services.Client.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class MessageServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly MessageServiceImpl messageService;

        public MessageServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            messageService = new MessageServiceImpl(restClient);
        }

        [Fact]
        public void GetMessages_Always_CallsExecutePost()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var request = new RestRequest("message", Method.POST);

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new List<Message>());
            restClient.Execute<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            messageService.GetMessages(messageType, fromTime, toTime, customers);

            // Assert
            restClient.Received().Execute<List<Message>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetMessages_ResponseIsSuccessful_ReturnsCorrectResult()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var expectedResult = new List<Message>
            {
                new Message { CustId = 1, Content = "Msg" }
            };

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(expectedResult);
            restClient.Execute<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            var actualResult = messageService.GetMessages(messageType, fromTime, toTime, customers);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetMessages_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.Execute<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => messageService.GetMessages(messageType, fromTime, toTime, customers));
        }

        [Fact]
        public void GetMessagesAsync_Always_CallsExecuteTaskAsync()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var request = new RestRequest("message", Method.POST);

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new List<Message>());
            restClient.ExecuteTaskAsync<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            messageService.GetMessagesAsync(messageType, fromTime, toTime, customers).ConfigureAwait(false);

            // Assert
            restClient.Received().ExecuteTaskAsync<List<Message>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetMessagesAsync_ResponseIsSuccessful_ReturnsCorrectResult()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var expectedResult = new List<Message>
            {
                new Message { CustId = 1, Content = "Msg" }
            };

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(expectedResult);
            restClient.ExecuteTaskAsync<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act
            var actualResult = messageService.GetMessagesAsync(messageType, fromTime, toTime, customers)
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetMessagesAsync_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var messageType = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient.ExecuteTaskAsync<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(Task.FromResult(restResponse));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => messageService.GetMessagesAsync(messageType, fromTime, toTime, customers));
        }
    }
}