using Confluent.Kafka;

namespace KitchenHell.Messaging.Kafka;

public class UInt64Serializer : ISerializer<ulong>
{
  public byte[] Serialize(ulong data, SerializationContext context)
  {
    return BitConverter.GetBytes(data);
  }
}
