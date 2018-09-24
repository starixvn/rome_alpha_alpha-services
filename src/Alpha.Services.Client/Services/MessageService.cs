namespace Alpha.Services.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Client.Models;
    using RestSharp;

    public interface MessageService
    {
        IEnumerable<Message> GetMessages(int messageType, DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers);

        Task<IEnumerable<Message>> GetMessagesAsync(int messageType, DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers);
    }

    public class MessageServiceImpl : BaseService, MessageService
    {
        public MessageServiceImpl()
            : this(null)
        {
        }

        internal MessageServiceImpl(IRestClient restClient)
            : base(restClient)
        {
        }

        public IEnumerable<Message> GetMessages(int messageType, DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers)
           => Post<List<Message>>("message",
               new
               {
                   messageType,
                   fromTime = fromTime.ToUnixTimeMilliseconds(),
                   toTime = toTime.ToUnixTimeMilliseconds(),
                   customers
               });

        public async Task<IEnumerable<Message>> GetMessagesAsync(int messageType, DateTimeOffset fromTime, DateTimeOffset toTime, IEnumerable<Customer> customers)
            => await PostAsync<List<Message>>("message",
                new
                {
                    messageType,
                    fromTime = fromTime.ToUnixTimeMilliseconds(),
                    toTime = toTime.ToUnixTimeMilliseconds(),
                    customers
                });
    }
}