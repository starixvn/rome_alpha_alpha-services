namespace Apha.Services.Tests.Services
{
    using System;
    using Alpha.Services.Configurations;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using Microsoft.Extensions.Options;
    using NSubstitute;
    using Xunit;

    public class MessageServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerDateLimitService customerDateLimitService;
        private readonly MessageServiceImpl messageService;

        public MessageServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerDateLimitService = Substitute.For<CustomerDateLimitService>();

            var options = Substitute.For<IOptions<AppSettings>>();
            var appSettings = new AppSettings
            {
                IsLocalTest = true,
                ParentSites = new ParentSiteSettings { Agent = "3000,3300" }
            };
            options.Value.Returns(appSettings);

            messageService = new MessageServiceImpl(dynamicRepository, customerDateLimitService, options.Value);
        }

        [Fact]
        public void GetMessages_Always_CallsCustomerDateLimitService()
        {
            // Arrange
            var param = new MessageParam(UserLevel.Agent, 1, 0, "en-US", DateTime.Now, DateTime.Now);

            // Act
            messageService.GetMessages(param).GetAwaiter().GetResult();

            // Assert
            customerDateLimitService.Received().GetCustomerDateLimit(Arg.Is(param.CustId));
        }

        [Fact]
        public void GetMessages_Always_CallsDynamicRepository()
        {
            // Arrange
            var param = new MessageParam(UserLevel.Agent, 1, 0, "en-US", DateTime.Now, DateTime.Now);

            // Act
            messageService.GetMessages(param).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().FetchAsync<Message>(Arg.Any<GetMessagesCriteria>());
        }
    }
}