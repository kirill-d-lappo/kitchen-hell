using System.Text.Json;
using Confluent.Kafka;

namespace KitchenHell.Messaging.Kafka;

internal class JsonSerializer<TValue> : ISerializer<TValue>
{
    private readonly JsonSerializerOptions _options;

    public JsonSerializer(JsonSerializerOptions options)
    {
        _options = options;
    }

    public JsonSerializer()
        : this(new JsonSerializerOptions())
    {
    }

    public byte[] Serialize(TValue data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}
