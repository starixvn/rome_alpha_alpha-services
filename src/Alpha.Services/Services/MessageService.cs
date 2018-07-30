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
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerDateLimitService customerDateLimitService;
        private readonly AppSettings appSettings;

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
            var custLevel = param.CustLevel;
            var parentSites = custLevel.IsSuperLevel()
                ? appSettings.ParentSites.Super
                : custLevel.IsMasterLevel()
                ? appSettings.ParentSites.Master
                : appSettings.ParentSites.Agent;

            var limitedDate = await customerDateLimitService.GetCustomerDateLimit(param.CustId);
            var fromTime = param.MessageType == Constants.MessageTypeConstants.Personal
                        ? param.FromTime.GetGreaterDate(limitedDate)
                        : param.FromTime;

            return await dynamicRepository.FetchAsync<Message>(
                  new GetMessagesCriteria(
                      param.CustId,
                      parentSites,
                      0,
                      fromTime,
                      param.ToTime,
                      appSettings.IsLocalTest,
                      param.MessageType));
        }
    }
}