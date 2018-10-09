namespace Alpha.Services.Client.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Helpers;
    using RestSharp;

    public interface LogService
    {
        Task WriteLog(
            int actorId,
            int custId,
            int actionId,
            string spName,
            string ip,
            IDictionary<string, object> newValues,
            IDictionary<string, object> oldValues);
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

        public async Task WriteLog(
            int actorId,
            int custId,
            int actionId,
            string spName,
            string ip,
            IDictionary<string, object> newValues,
            IDictionary<string, object> oldValues)
        {
            InputValidator.ValidateInputParam(nameof(spName), spName);
            InputValidator.ValidateInputParam(nameof(ip), ip);
            InputValidator.ValidateInputParam(nameof(newValues), newValues);

            await PostAsync(
                "Log",
                new
                {
                    actorId,
                    custId,
                    actionId,
                    spName,
                    ip,
                    newValues,
                    oldValues
                });
        }
    }
}