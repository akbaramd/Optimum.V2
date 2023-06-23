namespace Optimum.Api;

public class RequestContext<TRequest>
{
    public RequestPathParams PathParams { get; set; } = new();
    public RequestQueryStrings QueryStrings { get; set; } = new();
    public TRequest Body { get; set; }
}