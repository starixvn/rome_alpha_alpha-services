using System.Collections.Generic;

namespace Alpha.Services.Params
{
    public class LogParam
    {
        public LogParam(
            int actorId,
            int custId,
            int actionId,
            string spName,
            string ip,
            IDictionary<string, object> newValues,
            IDictionary<string, object> oldValues)
        {
            ActorId = actorId;
            CustId = custId;
            ActionId = actionId;
            SpName = spName;
            IP = ip;
            NewValues = newValues;
            OldValues = oldValues;
        }

        public int ActorId { get; }

        public int CustId { get; }

        public int ActionId { get; }

        public string SpName { get; }

        public string IP { get; }

        public IDictionary<string, object> NewValues { get; }

        public IDictionary<string, object> OldValues { get; }
    }
}