namespace Alpha.Services.API.Tests.Controllers
{
    using Alpha.Services.API.Controllers;
    using MediatR;
    using NSubstitute;

    public class ReportControllerTests
    {
        private readonly IMediator mediator;
        private readonly ReportController reportController;

        public ReportControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            reportController = new ReportController(mediator);
        }
    }
}