namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface MessageService
    {
        Task<IEnumerable<Message>> GetMessages(MessageParam param);
    }

    public class MessageServiceImpl : MessageService
    {
        private readonly IDynamicRepository dynamicRepository;

        public MessageServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<IEnumerable<Message>> GetMessages(MessageParam param)
            => await dynamicRepository.FetchAsync<Message>(
                new GetMessagesCriteria(
                    param.CustId,
                    param.ParentSites,
                    param.SpecialType,
                    param.TotalMessage,
                    param.StartDate,
                    param.EndDate,
                    param.IsTest,
                    param.MessageType));
    }
}