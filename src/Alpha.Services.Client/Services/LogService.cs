namespace Alpha.Services.Client.Services
{
    using System.Threading.Tasks;
    using Alpha.Services.Client.Helpers;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface LogService
    {
        Task WriteLog(ILogData logData);
    }

    public class LogServiceImpl : BaseService, LogService
    {
        public LogServiceImpl()
            : this(null)
        {
        }

        internal LogServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public async Task WriteLog(ILogData logData)
        {
            InputValidator.ValidateInputParam(nameof(logData.ActorId), logData.ActorId);
            InputValidator.ValidateInputParam(nameof(logData.CustId), logData.CustId);
            InputValidator.ValidateInputParam(nameof(logData.ActionId), logData.ActionId);
            InputValidator.ValidateInputParam(nameof(logData.SPName), logData.SPName);
            InputValidator.ValidateInputParam(nameof(logData.IP), logData.IP);
            InputValidator.ValidateInputParam(nameof(logData.NewValues), logData.NewValues);

            await PostAsync("Log",
                new
                {
                    actorId = logData.ActorId,
                    custId = logData.CustId,
                    actionId = logData.ActionId,
                    spName = logData.SPName,
                    ip = logData.IP,
                    newValues = logData.NewValues,
                    oldValues = logData.OldValues
                });
        }
    }
}