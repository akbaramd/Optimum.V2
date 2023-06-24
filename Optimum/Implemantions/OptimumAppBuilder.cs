using Microsoft.AspNetCore.Builder;
using Optimum.Contracts;

namespace Optimum.Implemantions;

public class OptimumAppBuilder : IOptimumAppBuilder
{
    public OptimumAppBuilder(WebApplication application)
    {
        Application = application;
    }

    public WebApplication Application { get; }
}