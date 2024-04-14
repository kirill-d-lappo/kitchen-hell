using Confluent.Kafka;

namespace KitchenHell.Messaging.Kafka;

public class UInt64Deserializer : IDeserializer<ulong>
{
  public ulong Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
  {
    var bytes = data.ToArray();
    if (BitConverter.IsLittleEndian)
    {
      bytes = bytes.Reverse().ToArray();
    }

    return BitConverter.ToUInt64(bytes);
  }
}
