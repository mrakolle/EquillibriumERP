namespace EquillibriumERP.Api.Tracing;

public class ExecutionTracingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestExecutionContext _trace;

    public ExecutionTracingMiddleware(RequestDelegate next, RequestExecutionContext trace)
    {
        _next = next;
        _trace = trace;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _trace.Step($"HTTP {context.Request.Method} {context.Request.Path}");

        await _next(context); // 🔥 REQUIRED

        _trace.Step("HTTP REQUEST COMPLETED");
        _trace.Dump();
    }
}