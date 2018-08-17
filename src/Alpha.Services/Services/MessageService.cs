namespace Alpha.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.Services.Configurations;
    using Alpha.Services.Criteria;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Fanex.Data.Repository;

    public interface MessageService
    {
        Task<IEnumerable<Message>> GetMessages(MessageParam param);
    }

    public class MessageServiceImpl : MessageService
    {
        private readonly AppSettings appSettings;
        private readonly IDynamicRepository dynamicRepository;

        public MessageServiceImpl(
            IDynamicRepository dynamicRepository,
            AppSettings appSettings)
        {
            this.dynamicRepository = dynamicRepository;
            this.appSettings = appSettings;
        }

        public async Task<IEnumerable<Message>> GetMessages(MessageParam param)
        {
            var parentSites = GetParentSites(param.CustLevel);

            return await dynamicRepository.FetchAsync<Message>(
                  new GetMessagesCriteria(
                      param.CustId,
                      parentSites,
                      1000,
                      param.FromTime,
                      param.ToTime,
                      appSettings.IsLocalTest,
                      param.MessageType));
        }

        private string GetParentSites(UserLevel custLevel)
        {
            if (custLevel.IsSuperLevel())
            {
                return appSettings.ParentSites.Super;
            }

            if (custLevel.IsMasterLevel())
            {
                return appSettings.ParentSites.Master;
            }

            return appSettings.ParentSites.Agent;
        }
    }
}