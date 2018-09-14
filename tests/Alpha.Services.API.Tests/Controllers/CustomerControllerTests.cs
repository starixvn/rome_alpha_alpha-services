namespace Alpha.Services.API.Tests.Controllers
{
    using Alpha.Services.API.Controllers;
    using Alpha.Services.API.Requests;
    using MediatR;
    using NSubstitute;
    using Xunit;

    public class CustomerControllerTests
    {
        private readonly IMediator mediator;
        private readonly CustomerController customerController;

        public CustomerControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            customerController = new CustomerController(mediator);
        }

        [Fact]
        public void IsAccountClosed_Always_CallsMediatorSendGetAccountStatusQuery()
        {
            // Arrange
            var custId = 1;
            var subAccountId = 0;

            // Act
            customerController.IsAccountClosed(custId, subAccountId).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(Arg.Is<GetAccountStatusQuery>(
                q => q.CustId == custId
                    && q.SubAccountId == subAccountId));
        }
    }
}