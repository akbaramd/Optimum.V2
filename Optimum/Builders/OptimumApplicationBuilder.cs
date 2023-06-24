using Microsoft.AspNetCore.Builder;
using Optimum.Contracts;

namespace Optimum.Builders;

public class OptimumApplicationBuilder : IOptimumApplicationBuilder
{
    public OptimumApplicationBuilder(WebApplication application)
    {
        Application = application;
    }

    public WebApplication Application { get; }

    public void Run()
    {
       

        Application.Run();
    }

    public Task RunAsync()
    {
        return Application.RunAsync();
    }
}