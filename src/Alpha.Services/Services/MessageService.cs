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
        private readonly CustomerDateLimitService customerDateLimitService;

        public MessageServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerDateLimitService customerDateLimitService,
            AppSettings appSettings)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerDateLimitService = customerDateLimitService;
            this.appSettings = appSettings;
        }

        public async Task<IEnumerable<Message>> GetMessages(MessageParam param)
        {
            var parentSites = GetParentSites(param.CustLevel);

            var limitedDate = await customerDateLimitService.GetCustomerDateLimit(param.CustId);
            var fromTime = param.MessageType == Constants.MessageTypeConstants.Personal
                        ? param.FromTime.GetGreaterDate(limitedDate)
                        : param.FromTime;

            return await dynamicRepository.FetchAsync<Message>(
                  new GetMessagesCriteria(
                      param.CustId,
                      parentSites,
                      1000,
                      fromTime,
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