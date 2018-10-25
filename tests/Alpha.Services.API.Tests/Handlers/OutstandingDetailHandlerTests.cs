namespace Alpha.Services.API.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using Alpha.Services.Utils;
    using KellermanSoftware.CompareNetObjects;
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

        [Fact]
        public void Handle_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var request = new OutstandingDetailQuery(1, CustLevel.Agent);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            IEnumerable<OutstandingDetail> outstandings = new List<OutstandingDetail>
            {
                new OutstandingDetail(1, "cust1", 1, 2),
                new OutstandingDetail(2, "cust2", 2, 3),
                new OutstandingDetail(3, "cust3", 3, 4)
            };
            outstandingDetailService.GetOutstandingDetails(Arg.Any<OutstandingDetailParam>())
                .Returns(Task.FromResult(outstandings));

            var total = OutstandingDetailHandler.AccummulateTotal(outstandings);

            var expectedViewModel = new OutstandingDetailViewModel(request.CustLevel.DisplayName, 0, total, outstandings.OrderByDescending(o => o.MemberOutstanding).Take(50));

            // Act
            var actualViewModel = outstandingDetailHandler.Handle(request, default(CancellationToken))
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }
    }
}