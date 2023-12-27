using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace KitchenHell.Messaging.Kafka.Generators.Receivers;

public static class SymbolExtensions
{
    public static bool HasAttribute(this ISymbol symbol, string atrributeName)
    {
        return symbol.GetAttributes()
            .Any(_ => _.AttributeClass?.ToDisplayString() == atrributeName);
    }

    public static AttributeData FindAttribute(this ISymbol symbol, string atrributeName)
    {
        return symbol.GetAttributes()
            .FirstOrDefault(_ => _.AttributeClass?.ToDisplayString() == atrributeName);
    }

    public static bool IsDerivedFromType(this INamedTypeSymbol symbol, string typeName)
    {
        if (symbol.Name == typeName)
        {
            return true;
        }

        if (symbol.BaseType == null)
        {
            return false;
        }

        return symbol.BaseType.IsDerivedFromType(typeName);
    }

    public static bool IsImplements(this INamedTypeSymbol symbol, string typeName)
    {
        return symbol.IsType(typeName)
               || symbol.Interfaces
                   .Append(symbol.BaseType)
                   .Where(i => i != null)
                   .Any(i => i.IsImplements(typeName));
    }

    public static bool IsType(this INamedTypeSymbol symbol, string typeName)
    {
        return symbol.Name == typeName;
    }
}
