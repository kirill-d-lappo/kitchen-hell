using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;

namespace KitchenHell.Messaging.Kafka;

public static class ConsumerBuilderExtensions
{
    private static JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(),
                },
            };

            return options;
        }
    }

    private static readonly UInt64Serializer UInt64Serializer = new();

    private static readonly UInt64Deserializer UInt64Deserializer = new();

    public static ConsumerBuilder<ulong, TValue> SetKeyUInt64Deserializer<TValue>(
        this ConsumerBuilder<ulong, TValue> builder
    )
    {
        return builder.SetKeyDeserializer(UInt64Deserializer);
    }

    public static ProducerBuilder<ulong, TValue> SetKeyUInt64Serializer<TValue>(
        this ProducerBuilder<ulong, TValue> builder
    )
    {
        return builder.SetKeySerializer(UInt64Serializer);
    }

    public static ConsumerBuilder<TKey, TValue> SetValueJsonDeserializer<TKey, TValue>(
        this ConsumerBuilder<TKey, TValue> builder,
        Action<JsonSerializerOptions> configure = default
    )
    {
        var options = JsonSerializerOptions;

        configure?.Invoke(options);

        return builder.SetValueDeserializer(new JsonDeserializer<TValue>(options));
    }

    public static ProducerBuilder<TKey, TValue> SetValueJsonSerializer<TKey, TValue>(
        this ProducerBuilder<TKey, TValue> builder,
        Action<JsonSerializerOptions> configure = default
    )
    {
        var options = JsonSerializerOptions;

        configure?.Invoke(options);

        return builder.SetValueSerializer(new JsonSerializer<TValue>(options));
    }
}