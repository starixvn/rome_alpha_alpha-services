namespace Alpha.Services.API.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class LogHandler : IRequestHandler<InsertLogCommand, bool>
    {
        private readonly LogService logService;

        public LogHandler(LogService logService)
        {
            this.logService = logService;
        }

        public async Task<bool> Handle(InsertLogCommand request, CancellationToken cancellationToken)
            => await logService.WriteLog(
                new LogParam(
                    request.ActorId,
                    request.CustId,
                    request.ActionId,
                    request.SpName,
                    request.IP,
                    request.NewValues,
                    request.OldValues));
    }
}