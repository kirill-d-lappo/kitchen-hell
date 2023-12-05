using KitchenHell.Messaging.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Messaging;

public static class MessagingRegistrationExtensions
{
    public static void AddMessagingServices<TKey, TValue>(this IServiceCollection services)
    {
        services.AddHostedService<MessageConsumeBackgroundService<TKey, TValue>>();
    }
}