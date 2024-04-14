using KitchenHell.Messaging.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KitchenHell.Messaging;

public static class MessagePublisherRegistrationExtensions
{
  public static IServiceCollection AddConfiguredMessageProducer<TMessageKey, TMessage>(
    this IServiceCollection services,
    string messageName = null
  )
    where TMessage : IMessage<TMessageKey>
  {
    AddConfiguredMessageProducer<TMessageKey, TMessage, MessageProducerOptions>(services, messageName);

    return services;
  }

  public static IServiceCollection AddConfiguredMessageProducer<TMessageKey, TMessage, TMessageProducerOptions>(
    this IServiceCollection services,
    string messageName = null
  )
    where TMessage : IMessage<TMessageKey>
    where TMessageProducerOptions : MessageProducerOptions
  {
    messageName ??= typeof(TMessage).Name;
    messageName = RemoveSuffix(messageName, "Message");

    services.AddOptions<TMessageProducerOptions>(messageName)
      .BindConfiguration("Messaging:Producers:Settings")
      .BindConfiguration($"Messaging:Producers:Topics:{messageName}")
      .ValidateDataAnnotations()
      .ValidateOnStart();

    services
      .AddScoped<IMessageProducer<TMessageKey, TMessage>,
        NamedConfiguredMessageProducer<TMessageKey, TMessage>>(sp =>
      {
        var produceService = sp.GetService<IMessageProduceService<TMessageKey, TMessage>>();
        var options = sp.GetService<IOptionsSnapshot<TMessageProducerOptions>>();

        return new NamedConfiguredMessageProducer<TMessageKey, TMessage>(produceService, options, messageName);
      });

    return services;
  }

  private static string RemoveSuffix(string value, string suffix)
  {
    if (value.EndsWith(suffix))
    {
      return value[..value.LastIndexOf(suffix, StringComparison.Ordinal)];
    }

    return value;
  }
}
