namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Criteria.Message;
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
        {
            return await dynamicRepository.FetchAsync<Message>(
                  new GetNewMessagesCriteria(
                      param.FromTime,
                      param.ToTime,
                      param.MessageType,
                      param.IsLocalTest));
        }
    }
}