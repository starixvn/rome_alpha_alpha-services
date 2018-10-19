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
    using Alpha.Services.Utils;
    using NSubstitute;
    using Xunit;

    public class OutstandingDetailHandlerTests
    {
        private readonly ProductService productService;
        private readonly OutstandingDetailService outstandingDetailService;
        private readonly OutstandingDetailHandler outstandingDetailHandler;

        public OutstandingDetailHandlerTests()
        {
            productService = Substitute.For<ProductService>();
            outstandingDetailService = Substitute.For<OutstandingDetailService>();
            outstandingDetailHandler = new OutstandingDetailHandler(productService, outstandingDetailService);
        }

        [Fact]
        public void Handle_Always_CallsGetOfferedProductIdsForCustomer()
        {
            // Arrange
            var request = new OutstandingDetailQuery(1, CustLevel.Agent);

            // Act
            outstandingDetailHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            productService.Received().GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);
        }

        [Fact]
        public void Handle_Always_CallsGetOutstandingDetails()
        {
            // Arrange
            var request = new OutstandingDetailQuery(1, CustLevel.Agent);
            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            var param = new OutstandingDetailParam(request.CustLevel, request.CustId, productIds);
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>()).Returns(Task.FromResult(productIds));

            // Act
            outstandingDetailHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            outstandingDetailService.Received().GetOutstandingDetails(Arg.Is<OutstandingDetailParam>(
                p => p.CustId == param.CustId && p.CustLevel == param.CustLevel && p.ProductIds == param.ProductIds));
        }
    }
}