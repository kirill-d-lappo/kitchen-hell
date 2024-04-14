using Microsoft.CodeAnalysis;

namespace KitchenHell.Messaging.Kafka.Generators.Receivers;

public class ClassesUnderNamespace : SyntaxReceiver
{
  private readonly string _namespaceName;

  public ClassesUnderNamespace(string namespaceName)
  {
    _namespaceName = namespaceName;
  }

  public override bool CollectClassSymbol => true;

  protected override bool ShouldCollectClassSymbol(INamedTypeSymbol classSymbol)
  {
    return classSymbol.ContainingNamespace != null
           && classSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.CSharpShortErrorMessageFormat)
             .StartsWith(_namespaceName);
  }
}
