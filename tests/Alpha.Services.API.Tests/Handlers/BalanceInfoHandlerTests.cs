namespace Alpha.Services.API.Tests.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Models;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class BalanceInfoHandlerTests
    {
        private readonly CustomerService customerService;
        private readonly BalanceInfoService balanceInfoService;
        private readonly BalanceInfoHandler balanceInfoHandler;

        public BalanceInfoHandlerTests()
        {
            customerService = Substitute.For<CustomerService>();
            balanceInfoService = Substitute.For<BalanceInfoService>();
            balanceInfoHandler = new BalanceInfoHandler(customerService, balanceInfoService);
        }

        [Fact]
        public void Handle_Always_CallsGetCustomerInfo()
        {
            // Arrange
            var request = new BalanceInfoQuery(1);

            // Act
            balanceInfoHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            customerService.Received().GetCustomerInfo(Arg.Is(request.CustId));
        }

        [Fact]
        public void Handle_CustomerIsNotNull_CallsGetBalanceInfo()
        {
            // Arrange
            var request = new BalanceInfoQuery(1);
            var customer = new Customer(1, "username", CustLevel.Agent, "UUS", 1);
            customerService.GetCustomerInfo(Arg.Any<int>())
                .Returns(Task.FromResult(customer));

            // Act
            balanceInfoHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            balanceInfoService.Received().GetBalanceInfo(Arg.Is(customer.CustLevel), Arg.Is(request.CustId));
        }
    }
}