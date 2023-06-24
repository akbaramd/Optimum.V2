using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Optimum.Contracts;

public interface IOptimumBuilder
{
    public string ServiceId { get;  }
    public string ServiceName { get;  }
    public IServiceCollection Services { get;  }
    public ILogger Logger { get;  }
}