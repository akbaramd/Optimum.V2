using Optimum.Abstractions;
using Serilog.Core;
using Serilog.Events;

namespace Optimum.Configures;

public class OptimumLogger : IOptimumLogger 
{
    public void Write(LogEvent logEvent)
    {
        throw new NotImplementedException();
    }
}