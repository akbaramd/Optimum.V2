namespace Optimum.WebApi.CQRS;

public static class Extensions
{
    // public static async Task MapPostCommand<TCommand>(this IEndpointConfigure configure, string pattern)
    //     where TCommand : ICommand
    // {
    //     var dispatcher = configure..Services.GetRequiredService<ICommandDispatcher>();
    //     
    //     configure.WebApplication.MapPost(pattern, async context =>
    //     {
    //         var requestBodyString = await new StreamReader(context.Request.Body).ReadToEndAsync();
    //         var requestBody = JsonConvert.DeserializeObject<TCommand>(requestBodyString) ??
    //                           throw new InvalidOperationException();
    //         await dispatcher.SendAsync<TCommand>(requestBody);
    //
    //         await context.Response.WriteAsJsonAsync(new { Status = true });
    //     });
    // }
}