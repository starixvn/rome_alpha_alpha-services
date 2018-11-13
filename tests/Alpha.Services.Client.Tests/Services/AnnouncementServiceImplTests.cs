namespace Alpha.Services.Client.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using NSubstitute;
    using RestSharp;
    using Xunit;

    public class AnnouncementServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly AnnouncementServiceImpl announcementService;

        public AnnouncementServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            announcementService = new AnnouncementServiceImpl(restClient);
        }

        [Fact]
        public void GetAnnouncements_Always_CallsExecutePost()
        {
            // Arrange
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var request = new RestRequest("announcements", Method.POST);

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(true);
            restResponse.Data.Returns(new List<Message>());
            restClient.Execute<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act
            announcementService.GetAnnouncements(fromTime, toTime, customers);

            // Assert
            restClient.Received().Execute<List<Message>>(Arg.Is<IRestRequest>(
                r => r.Resource == request.Resource
                    && r.Method == request.Method));
        }

        [Fact]
        public void GetAnnouncements_ResponseIsSuccessful_ReturnsCorrectResult()
        {
            // Arrange
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
            var actualResult = announcementService.GetAnnouncements(fromTime, toTime, customers);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetAnnouncements_ResponseIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;
            var customers = new List<Customer>
            {
                new Customer { CustId = 1, Language = "en-US" }
            };

            var restResponse = Substitute.For<IRestResponse<List<Message>>>();
            restResponse.IsSuccessful.Returns(false);
            restClient
                .Execute<List<Message>>(Arg.Any<IRestRequest>())
                .Returns(restResponse);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => announcementService.GetAnnouncements(fromTime, toTime, customers));
        }
    }
}