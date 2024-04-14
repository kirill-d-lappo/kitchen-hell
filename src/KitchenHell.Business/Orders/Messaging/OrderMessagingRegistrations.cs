using KitchenHell.Business.Messages;
using KitchenHell.Business.Orders.Messaging.Consumers;
using KitchenHell.Business.Restaurants.Messaging.Consumers;
using KitchenHell.Messaging.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Business.Orders.Messaging;

public static class OrderMessagingRegistrations
{
  public static void AddOrdersMessaging(this IServiceCollection services)
  {
    services.AddMessageProducer<string, OrderCreatedMessage>();
  }

  /// <summary>
  ///   Registers all messaging related handlers and consumers in KitchenHell.Business project.
  ///   <br />
  ///   Generated: 2024-04-14 11:23:52Z
  /// </summary>

  // FixMe [2024-04-14 klappo] from autogenerated
  public static IServiceCollection AddKitchenHellBusinessMessaging(this IServiceCollection services)
  {
    ////////////////////////////
    // Handlers Registrations //
    ////////////////////////////
    services.AddKafkaJsonConsumer<string, OrderRestaurantStatusUpdatedMessage>("OrderRestaurantStatusUpdated")
      .AddHandler<OrderRestaurantStatusUpdatedMessageHandler>();

    services.AddKafkaJsonConsumer<string, OrderStatusUpdatedMessage>("OrderStatusUpdated")
      .AddHandler<OrderStatusUpdatedMessageHandler>();

    services.AddKafkaJsonConsumer<string, OrderCreatedMessage>("OrderCreated")
      .AddHandler<OrderCreatedMessageRestaurantHandler>();

    // No Producers Registrations - Not Implemented Yet

    return services;
  }
}
