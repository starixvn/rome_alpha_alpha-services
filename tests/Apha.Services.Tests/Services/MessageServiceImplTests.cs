namespace Apha.Services.Tests.Services
{
    using System;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    public class MessageServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly MessageServiceImpl messageService;

        public MessageServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            messageService = new MessageServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetMessages_Always_CallsDynamicRepository()
        {
            // Arrange
            var param = new MessageParam(0, DateTime.Now, DateTime.Now, true);

            // Act
            messageService.GetMessages(param).GetAwaiter().GetResult();

            // Assert
            dynamicRepository.Received().FetchAsync<Message>(Arg.Any<GetNewMessagesCriteria>());
        }
    }
}