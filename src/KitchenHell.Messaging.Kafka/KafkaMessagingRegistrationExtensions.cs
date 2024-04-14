using Confluent.Kafka;
using KitchenHell.Messaging.Kafka.Consumers;
using KitchenHell.Messaging.Kafka.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KitchenHell.Messaging.Consumers;
using KitchenHell.Messaging.Producers;
using Microsoft.Extensions.Options;

namespace KitchenHell.Messaging.Kafka;

[Obsolete($"Use {nameof(MessagingRegistrationExtensions)}, this registration will be hidden")]
public static class KafkaMessagingRegistrationExtensions
{
  public static ConsumerHandlerBuilder<TKey, TValue> AddKafkaJsonConsumer<TKey, TValue>(
    this IServiceCollection services,
    string consumerName,
    Action<ConsumerBuilder<TKey, TValue>> configureConsumer = default
  )
  {
    return AddKafkaConsumer<TKey, TValue>(services, consumerName, cb =>
    {
      cb.SetValueJsonDeserializer();
      configureConsumer?.Invoke(cb);
    });
  }

  public static ConsumerHandlerBuilder<TKey, TValue> AddKafkaConsumer<TKey, TValue>(
    this IServiceCollection services,
    string consumerName,
    Action<ConsumerBuilder<TKey, TValue>> configureConsumer = default
  )
  {
    if (string.IsNullOrWhiteSpace(consumerName))
    {
      throw new ArgumentException("Can't be empty or default", nameof(consumerName));
    }

    services.AddOptions<KafkaConsumerOptions<TKey, TValue>>()
      .BindTopicConsumerOptions(consumerName);

    services.AddSingleton<IConsumer<TKey, TValue>>(sp =>
    {
      var configuration = sp.GetService<IConfiguration>();
      var kafkaConsumerConfig = GetKafkaConsumerConfig(configuration);
      var builder = new ConsumerBuilder<TKey, TValue>(kafkaConsumerConfig);
      configureConsumer?.Invoke(builder);

      return builder.Build();
    });

    services.AddScoped<IMessageConsumeService<TKey, TValue>, KafkaMessageConsumeService<TKey, TValue>>();

    services.AddMessagingServices<TKey, TValue>();

    return new ConsumerHandlerBuilder<TKey, TValue>
    {
      Services = services,
    };
  }

  public static void AddKafkaJsonProducer<TKey, TValue>(
    this IServiceCollection services,
    Action<ProducerBuilder<TKey, TValue>> configureProducer = default
  )
  {
    AddKafkaProducer<TKey, TValue>(services, pb =>
    {
      pb.SetValueJsonSerializer();
      configureProducer?.Invoke(pb);
    });
  }

  public static void AddKafkaProducer<TKey, TValue>(
    this IServiceCollection services,
    Action<ProducerBuilder<TKey, TValue>> configureProducer = default
  )
  {
    services.AddSingleton(sp =>
    {
      var configuration = sp.GetRequiredService<IConfiguration>();
      var kafkaConfig = GetKafkaProducerConfig(configuration);
      var builder = new ProducerBuilder<TKey, TValue>(kafkaConfig);
      configureProducer?.Invoke(builder);
      var consumer = builder.Build();

      return consumer;
    });

    services.AddSingleton<IMessageProduceService<TKey, TValue>, KafkaMessageProduceService<TKey, TValue>>();
  }

  public static void AddProducerOptions<TOption>(
    this IServiceCollection services,
    string name
  )
    where TOption : class
  {
    services.AddOptions<TOption>()
      .BindTopicProducerOptions(name);
  }

  private static OptionsBuilder<TOptions> BindTopicProducerOptions<TOptions>(
    this OptionsBuilder<TOptions> optionsBuilder,
    string name
  ) where TOptions : class
  {
    return optionsBuilder
      .BindProducerOptions("Settings")
      .BindProducerOptions($"Topics:{name}");
  }

  private static OptionsBuilder<TOptions> BindTopicConsumerOptions<TOptions>(
    this OptionsBuilder<TOptions> optionsBuilder,
    string name
  ) where TOptions : class
  {
    return optionsBuilder
      .BindConsumerOptions("Settings")
      .BindConsumerOptions($"Topics:{name}");
  }

  private static ConsumerConfig GetKafkaConsumerConfig(IConfiguration configuration)
  {
    var config = new ConsumerConfig();
    configuration.Bind("Messaging:Consumers:Settings:Kafka", config);

    return config;
  }

  private static ProducerConfig GetKafkaProducerConfig(IConfiguration configuration)
  {
    var config = new ProducerConfig();
    configuration.Bind("Messaging:Producers:Settings:Kafka", config);

    return config;
  }

  private static OptionsBuilder<TOptions> BindProducerOptions<TOptions>(
    this OptionsBuilder<TOptions> optionsBuilder,
    string name
  ) where TOptions : class
  {
    return BindMessagingOptions(optionsBuilder, "Producers", name);
  }

  private static OptionsBuilder<TOptions> BindConsumerOptions<TOptions>(
    this OptionsBuilder<TOptions> optionsBuilder,
    string name
  ) where TOptions : class
  {
    return BindMessagingOptions(optionsBuilder, "Consumers", name);
  }

  private static OptionsBuilder<TOptions> BindMessagingOptions<TOptions>(
    this OptionsBuilder<TOptions> optionsBuilder,
    string messagingRole,
    string name
  )
    where TOptions : class
  {
    return optionsBuilder.BindConfiguration($"Messaging:{messagingRole}:{name}");
  }

  public class ConsumerHandlerBuilder<TKey, TValue>
  {
    public IServiceCollection Services { get; init; }

    public ConsumerHandlerBuilder<TKey, TValue> AddHandler<THandler>()
      where THandler : class, IMessageHandler<TKey, TValue>
    {
      Services.AddScoped<IMessageHandler<TKey, TValue>, THandler>();

      return this;
    }
  }
}
