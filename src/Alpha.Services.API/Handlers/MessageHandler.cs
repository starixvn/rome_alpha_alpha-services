namespace Alpha.Services.API.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Configurations;
    using Alpha.Services.Constants;
    using Alpha.Services.Extensions;
    using Alpha.Services.Models;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class MessageHandler : IRequestHandler<MessageQuery, IEnumerable<MessageViewModel>>
    {
        private readonly MessageService messageService;
        private readonly AppSettings appSettings;

        public MessageHandler(MessageService messageService, AppSettings appSettings)
        {
            this.messageService = messageService;
            this.appSettings = appSettings;
        }

        public async Task<IEnumerable<MessageViewModel>> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var result = new List<MessageViewModel>();

            var param = new MessageParam(
                request.MessageType,
                request.FromTime.ToLocalTime(),
                request.ToTime.ToLocalTime(),
                appSettings.IsLocalTest);

            var messages = await messageService.GetMessages(param);

            Func<CustomerInfo, Message, bool> filterCustomer = (c, m)
                => m.MessageType == MessageTypeConstants.Personal
                    ? c.CustId == m.TargetId
                    : GetParentSites(c.CustLevel).Contains(m.TargetId);

            foreach (var message in messages)
            {
                var custMessages = request.Customers
                    .Where(customer => filterCustomer(customer, message))
                    .Select(customer => new MessageViewModel
                    {
                        CustId = customer.CustId,
                        Content = message.GetContentByLanguage(customer.Language).Replace("@@", message.BetTransId)
                    });

                result.AddRange(custMessages);
            }

            return result;
        }

        private IEnumerable<int> GetParentSites(UserLevel custLevel)
        {
            var parentSites = string.Empty;

            if (custLevel.IsSuperLevel())
            {
                parentSites = appSettings.ParentSites.Super;
            }

            if (custLevel.IsMasterLevel())
            {
                parentSites = appSettings.ParentSites.Master;
            }
            if (custLevel.IsAgentLevel())
            {
                parentSites = appSettings.ParentSites.Agent;
            }

            return parentSites.Split(',').Select(Int32.Parse).ToList();
        }
    }
}