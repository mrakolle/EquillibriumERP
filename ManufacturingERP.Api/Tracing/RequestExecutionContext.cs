namespace ManufacturingERP.Api.Tracing;

public class RequestExecutionContext
{
    private readonly ILogger<RequestExecutionContext> _logger;
    private readonly List<string> _steps = new();

    public RequestExecutionContext(ILogger<RequestExecutionContext> logger)
    {
        _logger = logger;
    }

    public void Step(string message)
    {
        _steps.Add(message);
        _logger.LogInformation("➡️ {Message}", message);
    }

    public void Dump()
    {
        _logger.LogInformation("📊 EXECUTION PATH:");
        foreach (var step in _steps)
        {
            _logger.LogInformation("   ↓ {Step}", step);
        }
    }
}