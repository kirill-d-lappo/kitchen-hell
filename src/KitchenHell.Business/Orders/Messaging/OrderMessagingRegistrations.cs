using KitchenHell.Business.Messages;
using KitchenHell.Business.Orders.Messaging.Consumers;
using KitchenHell.Business.Orders.Messaging.Producers;
using KitchenHell.Messaging.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Business.Orders.Messaging;

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
