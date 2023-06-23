using Microsoft.AspNetCore.Builder;
using Optimum.Abstractions;

namespace Optimum.Configures;

public class OptimumApplication : IOptimumApplication
{
    public OptimumApplication(WebApplication application)
    {
        Application = application;
    }

    public WebApplication Application { get; }
}