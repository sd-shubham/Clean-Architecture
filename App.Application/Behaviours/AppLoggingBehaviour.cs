using App.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace App.Application.Behaviours
{
    public class AppLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AppLoggingBehaviour<TRequest, TResponse>> _logger;
        public AppLoggingBehaviour(ILogger<AppLoggingBehaviour<TRequest, TResponse>> logger)
            => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";
            // this Log won't allocate any memory on heap.
            _logger.Log(requestNameWithGuid,"[START] {requestNameWithGuid}");
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                try
                {
                    _logger.Log(requestNameWithGuid, JsonSerializer.Serialize(request),"[PROPS] {requestNameWithGuid} {request}");
                }
                catch (NotSupportedException)
                {
                    _logger.Log(requestNameWithGuid,"[Serialization ERROR] {requestNameWithGuid} Could not serialize the request.");
                }

                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.Log(
                     requestNameWithGuid, stopwatch.ElapsedMilliseconds,
                    "[END] {requestNameWithGuid}; Execution time={ElapsedMilliseconds}ms");
            }

            return response;



        }
    }
}
