namespace Alpha.Services.API.Tests.Handlers
{
    using System.Threading;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
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
        public void Handle_Always_CallsCustomerServiceIsAccountClosed()
        {
            // Arrange
            var request = new GetAccountStatusQuery(1, 0);

            // Act
            customerHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received()
                .IsAccountClosed(Arg.Is(request.CustId), Arg.Is(request.SubAccountId));
        }
    }
}