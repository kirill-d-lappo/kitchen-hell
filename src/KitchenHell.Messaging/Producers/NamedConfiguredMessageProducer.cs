using Microsoft.Extensions.Options;

namespace KitchenHell.Messaging.Producers;

public class NamedConfiguredMessageProducer<TMessageKey, TMessage>
  : ConfiguredMessageProducer<TMessageKey, TMessage, MessageProducerOptions>
  where TMessage : IMessage<TMessageKey>
{
  private readonly string _messageName;
  private readonly IOptionsSnapshot<MessageProducerOptions> _optionsProvider;

  public NamedConfiguredMessageProducer(
    IMessageProduceService<TMessageKey, TMessage> produceService,
    IOptionsSnapshot<MessageProducerOptions> optionsProvider,
    string messageName
  )
    : base(produceService)
  {
    _optionsProvider = optionsProvider;
    _messageName = messageName;
  }

  protected override MessageProducerOptions ProducerOptions => _optionsProvider.Get(_messageName);
}
