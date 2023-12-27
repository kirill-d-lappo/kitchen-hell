namespace KitchenHell.Messaging.Kafka.Generators.Receivers;

internal class MessageHandlerClassesReceiver : ClassesWithInterfacesReceiver
{
    public MessageHandlerClassesReceiver()
        : base("IMessageHandler")
    {
    }
}
