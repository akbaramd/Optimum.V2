using Optimum.CQRS.Contracts;

namespace Optimum.Example.Service.Commands;

public class SendLogCommand : ICommand
{
    public string Log { get; set; }
}

public class SendLogCommandHandler : ICommandHandler<SendLogCommand>
{
    public Task HandleAsync(SendLogCommand command, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(command.Log);
        return Task.CompletedTask;
    }
}