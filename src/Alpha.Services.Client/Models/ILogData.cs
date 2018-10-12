namespace Alpha.Services.Client.Models
{
    using System.Collections.Generic;

    public interface ILogData
    {
        int ActorId { get; }

        int CustId { get; }

        int ActionId { get; }

        string SPName { get; }

        string IP { get; }

        IDictionary<string, object> NewValues { get; }

        IDictionary<string, object> OldValues { get; }
    }
}