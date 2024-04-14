namespace KitchenHell.Messaging.Kafka.Consumers;

public sealed record KafkaConsumerOptions<TKey, TValue>
{
  public bool IsEnabled { get; init; } = true;

  public string TopicName { get; init; }

  public int MaxSubscribeRetryCount { get; set; } = 5;
}
