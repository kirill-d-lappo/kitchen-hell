using Confluent.Kafka;
using KitchenHell.Messaging.Kafka.Consumers;
using KitchenHell.Messaging.Kafka.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KitchenHell.Messaging.Consumers;
using KitchenHell.Messaging.Producers;

namespace KitchenHell.Messaging.Kafka;

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
            .Configure((KafkaConsumerOptions<TKey, TValue> o, IConfiguration c) =>
            {
                BindTopicConsumerOptions(c, consumerName, o);
            });

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
            var configuration = sp.GetService<IConfiguration>();
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
            .Configure((TOption o, IConfiguration configuration) =>
            {
                BindProducerOptions(configuration, "Settings", o);
                BindProducerOptions(configuration, $"Topics:{name}", o);
            });
    }

    private static void BindTopicConsumerOptions(IConfiguration configuration, string name, object options)
    {
        BindConsumerOptions(configuration, "Settings", options);
        BindConsumerOptions(configuration, $"Topics:{name}", options);
    }

    private static ConsumerConfig GetKafkaConsumerConfig(IConfiguration configuration)
    {
        var config = new ConsumerConfig();
        BindConsumerOptions(configuration, "Settings:Kafka", config);

        return config;
    }

    private static ProducerConfig GetKafkaProducerConfig(IConfiguration configuration)
    {
        var config = new ProducerConfig();
        BindProducerOptions(configuration, "Settings:Kafka", config);

        return config;
    }

    private static void BindProducerOptions(IConfiguration configuration, string name, object options)
    {
        BindMessagingOptions(configuration, "Producers", name, options);
    }

    private static void BindConsumerOptions(IConfiguration configuration, string name, object options)
    {
        BindMessagingOptions(configuration, "Consumers", name, options);
    }

    private static void BindMessagingOptions(
        IConfiguration configuration,
        string messagingRole,
        string name,
        object options
    )
    {
        configuration.GetSection($"Messaging:{messagingRole}:{name}")
            ?.Bind(options);
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
