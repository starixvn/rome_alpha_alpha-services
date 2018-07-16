namespace Alpha.Services.API.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return await next();
        }
    }
}