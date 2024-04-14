using Microsoft.Extensions.DependencyInjection;

namespace KitchenHell.Messaging.Kafka;

public static class MessagingRegistrationExtensions
{
  public static void AddMessageProducer<TKey, TMessage>(this IServiceCollection services)
    where TMessage : IMessage<TKey>
  {
    services.AddConfiguredMessageProducer<TKey, TMessage>();
    services.AddKafkaJsonProducer<TKey, TMessage>();

    // services.AddProducerOptions<OrderCreatedProducerOptions>(OrderCreatedProducerOptions.Name);
  }
}
