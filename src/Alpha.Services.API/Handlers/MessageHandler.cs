namespace Alpha.Services.API.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.Services.API.Requests;
    using Alpha.Services.API.ViewModels;
    using Alpha.Services.Params;
    using Alpha.Services.Services;
    using MediatR;

    public class MessageHandler : IRequestHandler<MessageQuery, IEnumerable<MessageViewModel>>
    {
        private readonly MessageService messageService;

        public MessageHandler(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task<IEnumerable<MessageViewModel>> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var param = new MessageParam(
                request.CustId,
                request.MessageType,
                request.Language,
                request.FromTime,
                request.ToTime);

            var messages = await messageService.GetMessages(param);

            return messages.Select(message => new MessageViewModel(message.Id, message.GetContentByKey(param.Language)));
        }
    }
}