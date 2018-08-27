namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Infrastructure.Exceptions;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Extensions;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class MessageHandler : IRequestHandler<MessageQuery, IEnumerable<MessageViewModel>>
    {
        private readonly MessageService messageService;
        private readonly CustomerService customerService;

        public MessageHandler(
            MessageService messageService,
            CustomerService customerService)
        {
            this.messageService = messageService;
            this.customerService = customerService;
        }

        public async Task<IEnumerable<MessageViewModel>> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerService.GetCustomerInfo(request.CustId);

            if (customer == null)
            {
                throw new DomainException("Customer does not exist");
            }

            var param = new MessageParam(
                customer.CustLevel,
                request.CustId,
                request.MessageType,
                request.Language,
                request.FromTime,
                request.ToTime);

            var messages = await messageService.GetMessages(param);

            return messages.Select(
                message => new MessageViewModel
                {
                    Id = message.Id,
                    Content = message.GetContentByLanguage(param.Language)
                    .Replace("@@", message.BetTransId)
                    .StripHTML()
                });
        }
    }
}