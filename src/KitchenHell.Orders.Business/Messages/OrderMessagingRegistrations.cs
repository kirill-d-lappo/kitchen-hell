using KitchenHell.Messaging.Kafka;
using KitchenHell.Orders.Business.Messages.Consumers;
using KitchenHell.Orders.Business.Messages.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Orders.Business.Messages;

public static class OrderMessagingRegistrations
{
    public static void AddOrdersMessaging(this IServiceCollection services)
    {
        services.AddKafkaJsonConsumer<string, OrderStatusUpdatedMessage>("OrderStatusUpdates")
            .AddHandler<OrderStatusUpdatedMessageHandler>();

        services.AddKafkaJsonConsumer<string, OrderRestaurantStatusUpdatedMessage>("OrderRestaurantStatusUpdates")
            .AddHandler<OrderRestaurantStatusUpdatedMessageHandler>();

        services.AddKafkaJsonProducer<string, OrderCreatedMessage>();

        services.AddProducerOptions<OrderCreatedProducerOptions>(OrderCreatedProducerOptions.Name);
        services.AddScoped<IOrderCreatedMessageProducer, OrderCreatedMessageProducer>();
    }
}