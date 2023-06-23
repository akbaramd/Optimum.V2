using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace Optimum.Abstractions;

public interface IOptimumService
{
    public string ServiceId { get;  }
    public string ServiceName { get;  }
    public IServiceCollection Services { get;  }
    public ILogger Logger { get;  }
}