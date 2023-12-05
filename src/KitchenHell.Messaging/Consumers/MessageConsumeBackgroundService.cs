using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KitchenHell.Messaging.Consumers;

internal sealed class MessageConsumeBackgroundService<TKey, TValue> : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MessageConsumeBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Bug [2023/06/12 klappo] https://github.com/dotnet/runtime/issues/36063
        await Task.Yield();

        await using var scope = _serviceProvider.CreateAsyncScope();
        var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumeService<TKey, TValue>>();
        await consumer.ConsumeAsync(stoppingToken);
    }
}