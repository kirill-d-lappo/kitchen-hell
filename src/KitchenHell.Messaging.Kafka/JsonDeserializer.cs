using System.Text.Json;
using Confluent.Kafka;

namespace KitchenHell.Messaging.Kafka;

internal class JsonDeserializer<TValue> : IDeserializer<TValue>
{
    private readonly JsonSerializerOptions _options;

    public JsonDeserializer(JsonSerializerOptions options)
    {
        _options = options;
    }

    public JsonDeserializer()
        : this(new JsonSerializerOptions())
    {
    }

    public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<TValue>(data, _options);
    }
}
