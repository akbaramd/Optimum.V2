using Microsoft.AspNetCore.Builder;

namespace Optimum.Contracts;

public interface IOptimumApplicationBuilder
{
    public WebApplication Application { get;  }

    public void Run();
    public Task RunAsync();
}