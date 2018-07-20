namespace Alpha.Services.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;

    public interface IAlphaServicesClient
    {
        OutstandingModel GetOutstanding(int custId);

        BalanceInfo GetBalanceInfo(int custId);

        WinLoss GetWinLoss(int custId, DateTimeOffset date);

        IEnumerable<Message> GetMessages(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime);

        Task<OutstandingModel> GetOutstandingAsync(int custId);

        Task<BalanceInfo> GetBalanceInfoAsync(int custId);

        Task<WinLoss> GetWinLossAsync(int custId, DateTimeOffset date);

        Task<IEnumerable<Message>> GetMessagesAsync(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}