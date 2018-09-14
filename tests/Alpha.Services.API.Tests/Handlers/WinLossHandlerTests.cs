namespace Alpha.Services.API.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using NSubstitute;
    using Xunit;

    public class WinLossHandlerTests
    {
        private readonly ProductService productService;
        private readonly WinLossService winLossService;
        private readonly WinLossHandler winLossHandler;

        public WinLossHandlerTests()
        {
            productService = Substitute.For<ProductService>();
            winLossService = Substitute.For<WinLossService>();
            winLossHandler = new WinLossHandler(productService, winLossService);
        }

        [Fact]
        public void Handle_Always_CallsGetOfferedProductIdsForCustomer()
        {
            // Arrange
            var request = new WinLossQuery(1, CustLevel.Agent, DateTime.Now);

            // Act
            winLossHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            productService.Received().GetOfferedProductIdsForCustomer(Arg.Is(request.CustId), Arg.Is(request.MinDate));
        }

        [Fact]
        public void Handle_Always_CallsGetWinLoss()
        {
            // Arrange
            var request = new WinLossQuery(1, CustLevel.Agent, DateTime.Now);
            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            var param = new WinLossParam(request.CustLevel, request.CustId, request.Date, productIds);

            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            // Act
            winLossHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            winLossService.Received().GetWinLoss(Arg.Is<WinLossParam>(
                p => p.CustLevel == param.CustLevel
                    && p.CustId == param.CustId
                    && p.Date == param.Date
                    && p.ProductIds == param.ProductIds));
        }
    }
}