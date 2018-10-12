namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface LogService
    {
        Task<bool> WriteLog(LogParam param);
    }

    public class LogServiceImpl : LogService
    {
        private readonly IDynamicRepository dynamicRepository;

        public LogServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<bool> WriteLog(LogParam param)
        {
            var trackingValue = BuildTrackingValue(param.SpName, param.IP, param.NewValues);

            var trackingValue2 = param.OldValues != null && param.OldValues.Any()
                ? BuildTrackingValue(param.SpName, param.IP, param.OldValues)
                : null;

            await dynamicRepository.ExecuteAsync(
                  new InsertUserLogCommand(
                      param.ActorId,
                      param.CustId,
                      param.ActionId,
                      trackingValue,
                      trackingValue2,
                      0)).ConfigureAwait(false);

            return true;
        }

        private static string BuildTrackingValue(string spName, string ip, IDictionary<string, object> values)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"<{spName} ");

            foreach (var value in values)
            {
                stringBuilder.Append($"{value.Key}=\"{value.Value}\" ");
            }

            stringBuilder.Append($"IP=\"{ip}\" />");

            return stringBuilder.ToString();
        }
    }
}