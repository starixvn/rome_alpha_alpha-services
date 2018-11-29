namespace Alpha.Services.API.Tests.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class CustomerHandlerTests
    {
        private readonly CustomerService customerService;
        private readonly CustomerHandler customerHandler;

        public CustomerHandlerTests()
        {
            customerService = Substitute.For<CustomerService>();
            customerHandler = new CustomerHandler(customerService);
        }

        [Fact]
        public void HandleGetAccountStatusQuery_Always_CallsCustomerServiceIsAccountClosed()
        {
            // Arrange
            var request = new GetAccountStatusQuery(1, 0);

            // Act
            customerHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().IsAccountClosed(Arg.Is(request.CustId), Arg.Is(request.SubAccountId));
        }

        [Fact]
        public void HandleDownlineInfoQuery_Always_CallsCustomerServiceGetDownlineInfo()
        {
            // Arrange
            var request = new DownlineInfoQuery(1, "username");

            // Act
            customerHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().GetDownlineInfo(Arg.Is(request.CustId), Arg.Is(request.DownlineUserName));
        }

        [Fact]
        public void HandleDownlineInfoQuery_Always_ReturnsCorrectResult()
        {
            // Arrange
            var request = new DownlineInfoQuery(1, "username");
            var downlineInfo = new DownlineInfo(2, false, true, 3);

            customerService.GetDownlineInfo(request.CustId, request.DownlineUserName)
                .Returns(Task.FromResult(downlineInfo));

            // Act
            var actualResult = customerHandler.Handle(request, default(CancellationToken)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(downlineInfo, actualResult);
        }

        [Fact]
        public void HandlerGetCustomerQuery_Alwyas_CallsGetCustomerInfo()
        {
            // Arrange
            var request = new GetCustomerQuery(1);

            // Act
            customerHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().GetCustomerInfo(request.CustId);
        }
    }
}