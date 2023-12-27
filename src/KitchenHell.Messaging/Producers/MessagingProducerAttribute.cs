namespace KitchenHell.Messaging.Producers;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MessagingProducerAttribute : Attribute
{
    public MessagingProducerAttribute(Type messageKeyType)
    {
        MessageKeyType = messageKeyType;
    }

    public Type MessageKeyType { get; set; }
}
