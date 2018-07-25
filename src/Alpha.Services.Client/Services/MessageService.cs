namespace Alpha.Services.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;

    public interface MessageService
    {
        IEnumerable<Message> GetMessages(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime);

        Task<IEnumerable<Message>> GetMessagesAsync(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class MessageServiceImpl : BaseService, MessageService
    {
        public IEnumerable<Message> GetMessages(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime)
           => Get<List<Message>>($"message/{custId}/messages/{messageType}/{language}/{fromTime.ToUnixTimeMilliseconds()}/{toTime.ToUnixTimeMilliseconds()}");

        public async Task<IEnumerable<Message>> GetMessagesAsync(int custId, int messageType, string language, DateTimeOffset fromTime, DateTimeOffset toTime)
            => await GetAsync<List<Message>>($"message/{custId}/messages/{messageType}/{language}/{fromTime.ToUnixTimeMilliseconds()}/{toTime.ToUnixTimeMilliseconds()}");
    }
}