using Microsoft.CodeAnalysis;

namespace KitchenHell.Messaging.Kafka.Generators.Receivers;

public class ClassesWithAttribute : SyntaxReceiver
{
    private readonly string _attributeType;

    public ClassesWithAttribute(string attributeType)
    {
        _attributeType = attributeType;
    }

    public override bool CollectClassSymbol => true;

    protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
    {
        return classSymbol.HasAttribute(_attributeType);
    }
}
