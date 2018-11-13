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
        private readonly IAppSettings appSettings;
        private readonly MessageService messageService;
        private readonly CustomerService customerService;

        public MessageHandler(
            IAppSettings appSettings,
            MessageService messageService,
            CustomerService customerService)
        {
            this.appSettings = appSettings;
            this.messageService = messageService;
            this.customerService = customerService;
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

            foreach (var message in messages)
            {
                var custMessages = request.Customers
                    .Where(customer => IsMessageBelongCustomer(customer, message))
                    .Select(customer => new MessageViewModel
                    {
                        CustId = customer.CustId,
                        Content = message.GetContentByLanguage(customer.Language).Replace("@@", message.BetTransId)
                    });

                result.AddRange(custMessages);
            }

            return result;
        }

        private bool IsMessageBelongCustomer(CustomerInfo customer, Message message)
            => message.MessageType == MessageTypeConstants.Personal
                ? customer.CustId == message.TargetId
                : GetCustomerParentSites(customer.CustId).Contains(message.TargetId);

        private IEnumerable<int> GetCustomerParentSites(int custId)
        {
            var customer = customerService.GetCustomerInfo(custId).GetAwaiter().GetResult();

            return appSettings.GetParentSites(customer.CustLevel.DisplayName, customer.System);
        }
    }
}