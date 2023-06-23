using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Optimum.Abstractions;

public interface IOptimumApplication
{
    public WebApplication Application { get;  }
}