using Microsoft.AspNetCore.Builder;

namespace Optimum.Contracts;

public interface IOptimumAppBuilder
{
    public WebApplication Application { get;  }
}