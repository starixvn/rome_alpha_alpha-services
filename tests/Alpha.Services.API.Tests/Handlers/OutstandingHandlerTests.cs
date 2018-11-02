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

    public class OutstandingHandlerTests
    {
        private const int DefaultTopDownlinesDisplay = 50;

        private readonly ProductService productService;
        private readonly OutstandingService outstandingService;
        private readonly OutstandingHandler outstandingHandler;

        public OutstandingHandlerTests()
        {
            productService = Substitute.For<ProductService>();
            outstandingService = Substitute.For<OutstandingService>();
            outstandingHandler = new OutstandingHandler(productService, outstandingService);
        }

        [Fact]
        public void Handle_Always_CallsGetOfferedProductIdsForCustomer()
        {
            // Arrange
            var request = new OutstandingQuery(1, CustLevel.Agent, DefaultTopDownlinesDisplay);

            // Act
            outstandingHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            productService.Received().GetOfferedProductIdsForCustomer(request.CustId, SystemTime.Today);
        }

        [Fact]
        public void Handle_Always_CallsGetOutstandings()
        {
            // Arrange
            var request = new OutstandingQuery(1, CustLevel.Agent, DefaultTopDownlinesDisplay);
            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            var param = new OutstandingParam(request.CustLevel, request.CustId, productIds);
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>()).Returns(Task.FromResult(productIds));

            // Act
            outstandingHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            outstandingService.Received().GetOutstandings(Arg.Is<OutstandingParam>(
                p => p.CustId == param.CustId && p.CustLevel == param.CustLevel && p.ProductIds == param.ProductIds));
        }

        [Fact]
        public void Handle_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var request = new OutstandingQuery(1, CustLevel.Agent, DefaultTopDownlinesDisplay);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            IEnumerable<OutstandingModel> outstandings = new List<OutstandingModel>
            {
                new OutstandingModel(1, "cust1", 1, 2),
                new OutstandingModel(2, "cust2", 2, 3),
                new OutstandingModel(3, "cust3", 3, 4)
            };
            outstandingService.GetOutstandings(Arg.Any<OutstandingParam>())
                .Returns(Task.FromResult(outstandings));

            var total = OutstandingHandler.AccummulateTotal(outstandings);

            var expectedViewModel = new OutstandingViewModel(request.CustLevel.Value, 0, total, outstandings.OrderByDescending(o => o.MemberOutstanding).Take(50));

            // Act
            var actualViewModel = outstandingHandler.Handle(request, default(CancellationToken))
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleDownlineOutstanding_Always_CallsGetDownlineOutstanding()
        {
            // Arrange
            var request = new DownlineOutstandingQuery(1, CustLevel.Agent, 2);
            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            var param = new OutstandingParam(request.CustLevel, request.CustId, productIds, request.DownlineId);
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>()).Returns(Task.FromResult(productIds));

            // Act
            outstandingHandler.Handle(request, default(CancellationToken)).ConfigureAwait(false);

            // Assert
            outstandingService.Received().GetDownlineOutstanding(Arg.Is<OutstandingParam>(
                p => p.CustId == param.CustId && p.CustLevel == param.CustLevel && p.ProductIds == param.ProductIds && p.DownlineId == param.DownlineId));
        }

        [Fact]
        public void HandleDownlineOutstanding_Always_ReturnsCorrectDictionary()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var request = new DownlineOutstandingQuery(1, CustLevel.Agent, 2);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            var outstanding = new OutstandingModel(2, "cust1", 1, 2);
            outstandingService.GetDownlineOutstanding(Arg.Any<OutstandingParam>())
                .Returns(Task.FromResult(outstanding));

            var expectedViewModel = new Dictionary<string, object>
            {
                ["Username"] = outstanding.CustName,
                [UserLevel.Member.DisplayName] = outstanding.MemberOutstanding,
                [request.CustLevel.DisplayName] = outstanding.Outstanding
            };

            // Act
            var actualViewModel = outstandingHandler.Handle(request, default(CancellationToken))
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleDownlineOutstanding_InvalidDownlineId_ReturnsEmptyDictionary()
        {
            // Arrange
            var request = new DownlineOutstandingQuery(1, CustLevel.Agent, 0);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            OutstandingModel outstanding = null;
            outstandingService.GetDownlineOutstanding(Arg.Any<OutstandingParam>())
                .Returns(Task.FromResult(outstanding));

            // Act
            var actualViewModel = outstandingHandler.Handle(request, default(CancellationToken))
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(0, actualViewModel.Count);
        }
    }
}