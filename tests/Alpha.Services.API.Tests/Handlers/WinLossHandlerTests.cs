namespace Alpha.Services.API.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.Tests.Constants;
    using Alpha.Services.Models;
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

        [Fact]
        public void Handle_CusLevelIsAgent_ReturnsCorrectResult()
        {
            // Assert
            var request = new WinLossQuery(1, CustLevel.Agent, DateTime.Now);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            var winLoss = new WinLossModel(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            winLossService.GetWinLoss(Arg.Any<WinLossParam>())
                .Returns(Task.FromResult(winLoss));

            var expectedResult = new Dictionary<string, object>
            {
                [nameof(winLoss.Turnover)] = winLoss.Turnover,
                [nameof(winLoss.GrossComm)] = winLoss.GrossComm,
                ["MemberNetWinLoss.WinLoss"] = winLoss.MemberWinLoss,
                ["MemberNetWinLoss.Comm"] = winLoss.MemberComm,
                ["MemberNetWinLoss.Total"] = winLoss.MemberTotal,
                ["AgentNetWinLoss.WinLoss"] = winLoss.AgentWinLoss,
                ["AgentNetWinLoss.Comm"] = winLoss.AgentComm,
                ["AgentNetWinLoss.Total"] = winLoss.AgentWinLoss + winLoss.AgentComm,
                ["companyWinLoss"] = -(winLoss.AgentWinLoss + winLoss.AgentComm + winLoss.MemberTotal)
            };

            // Act
            var actualResult = winLossHandler.Handle(request, default(CancellationToken))
                .GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Handle_CusLevelIsMaster_ReturnsCorrectResult()
        {
            // Assert
            var request = new WinLossQuery(1, CustLevel.Master, DateTime.Now);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            var winLoss = new WinLossModel(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            winLossService.GetWinLoss(Arg.Any<WinLossParam>())
                .Returns(Task.FromResult(winLoss));

            var expectedResult = new Dictionary<string, object>
            {
                [nameof(winLoss.Turnover)] = winLoss.Turnover,
                [nameof(winLoss.GrossComm)] = winLoss.GrossComm,
                ["AgentNetWinLoss.Total"] = winLoss.AgentTotal,
                ["MasterNetWinLoss.WinLoss"] = winLoss.MasterWinLoss,
                ["MasterNetWinLoss.Comm"] = winLoss.MasterComm,
                ["MasterNetWinLoss.Total"] = winLoss.MasterWinLoss + winLoss.MasterComm,
                ["companyWinLoss"] = -(winLoss.AgentTotal + winLoss.MasterWinLoss + winLoss.MasterComm)
            };

            // Act
            var actualResult = winLossHandler.Handle(request, default(CancellationToken)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Handle_CusLevelIsSuper_ReturnsCorrectResult()
        {
            // Assert
            var request = new WinLossQuery(1, CustLevel.Super, DateTime.Now);

            IEnumerable<int> productIds = new List<int> { 1, 2, 3 };
            productService.GetOfferedProductIdsForCustomer(Arg.Any<int>(), Arg.Any<DateTime?>())
                .Returns(Task.FromResult(productIds));

            var winLoss = new WinLossModel(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            winLossService.GetWinLoss(Arg.Any<WinLossParam>())
                .Returns(Task.FromResult(winLoss));

            var expectedResult = new Dictionary<string, object>
            {
                [nameof(winLoss.Turnover)] = winLoss.Turnover,
                [nameof(winLoss.GrossComm)] = winLoss.GrossComm,
                ["MasterNetWinLoss.Total"] = winLoss.MasterTotal,
                ["SuperNetWinLoss.WinLoss"] = winLoss.SuperWinLoss,
                ["SuperNetWinLoss.Comm"] = winLoss.SuperComm,
                ["SuperNetWinLoss.Total"] = winLoss.SuperTotal,
                ["companyWinLoss"] = -(winLoss.MasterTotal + winLoss.SuperTotal)
            };

            // Act
            var actualResult = winLossHandler.Handle(request, default(CancellationToken)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}