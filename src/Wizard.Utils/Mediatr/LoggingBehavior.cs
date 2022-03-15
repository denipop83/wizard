using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace wizard.Utils.Mediatr;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = request.GetType().FullName;

        var requestJson = JsonSerializer.Serialize(request, Json.SerializerOptions);

        _logger.LogInformation("[START] RequestName: {RequestName}", requestName);

        TResponse response;
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("[PROPS] RequestName: {RequestName}; Request: {Request}", requestName, requestJson);
            response = await next();

            if (response is not Unit)
            {
                _logger.LogInformation("[RESP] RequestName: {RequestName}; Response: {Request}", requestName,
                    JsonSerializer.Serialize(response, Json.SerializerOptions));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("[Request ERROR] RequestName: {RequestName}; Exception message: {ExceptionMessage}", requestJson, ex.Message);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("[END] RequestName: {RequestName}; Execution time={ExecutionTimeInMilliseconds}ms", requestName,
                stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}