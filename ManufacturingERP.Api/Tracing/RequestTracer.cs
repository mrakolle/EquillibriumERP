
namespace ManufacturingERP.Api.Tracing;
public interface IRequestTracer
{
    void Step(string message);
}

public class RequestTracer : IRequestTracer
{
    private readonly ILogger<RequestTracer> _logger;

    public RequestTracer(ILogger<RequestTracer> logger)
    {
        _logger = logger;
    }

    public void Step(string message)
    {
        _logger.LogInformation("➡️ {Message}", message);
    }
}