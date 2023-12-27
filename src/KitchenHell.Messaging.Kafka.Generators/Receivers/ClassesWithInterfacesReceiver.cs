using Microsoft.CodeAnalysis;

namespace KitchenHell.Messaging.Kafka.Generators.Receivers;

public class ClassesWithInterfacesReceiver : SyntaxReceiver
{
    private readonly string _implementedInterface;

    public ClassesWithInterfacesReceiver(string implementedInterface)
    {
        _implementedInterface = implementedInterface;
    }

    public override bool CollectClassSymbol => true;

    protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
    {
        return classSymbol.IsImplements(_implementedInterface);
    }
}
