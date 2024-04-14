﻿using System.Text;
using KitchenHell.Messaging.Kafka.Generators.Receivers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace KitchenHell.Messaging.Kafka.Generators;

// Bug [2023-12-21 klappo] it doesn't recognizes types out of target assembly
// Bug [2023-12-21 klappo] IMessageHandler<string, global::KitchenHell.Business.Messages.OrderRestaurantStatusUpdatedMessage>
// Bug [2023-12-21 klappo] IMessageHandler
// Bug [2023-12-21 klappo] ErrorType
// Bug [2023-12-21 klappo] =====
// Bug [2023-12-21 klappo] no information about interface!
[Generator]
public class MessagingRegistrationsSourceGenerator : ISourceGenerator
{
  public void Initialize(GeneratorInitializationContext context)
  {
    // FixMe [2024-04-14 klappo] disabled until completing registration methods
    return;

    context.RegisterForSyntaxNotifications(() => new MessageHandlerClassesReceiver());

    // Bug [2023-12-26 klappo] I can't make them work both at the same time
    // context.RegisterForSyntaxNotifications(() => new MessageProducerClassesReceiver());
  }

  public void Execute(GeneratorExecutionContext context)
  {
    // FixMe [2024-04-14 klappo] disabled until completing registration methods
    return;

    var compilation = context.Compilation;
    var assembly = compilation.Assembly;
    var rootNamespace = assembly.Name;
    var targetDisplayName = assembly.Name.Replace(".", "");

    var cb = new SourceStringBuilder();

    cb.AppendLine("// <auto-generated/>");
    cb.AppendLine();
    cb.AppendLine("using System;");
    cb.AppendLine("using Microsoft.Extensions.DependencyInjection;");
    cb.AppendLine("using KitchenHell.Messaging.Kafka;");
    cb.AppendLine();

    cb.AppendFormatLine("namespace {0};", rootNamespace);
    cb.AppendLine();

    cb.AppendFormatLine("public static class {0}MessagingRegistrations", targetDisplayName);
    cb.AppendLine("{");

    using (cb.StartTabulationToRight())
    {
      cb.AppendLine("/// <summary>");
      cb.AppendFormatLine(
        "/// Registers all messaging related handlers and consumers in {0} project.",
        assembly.Name);

      cb.AppendLine("/// <br/>");
      cb.AppendFormatLine("/// Generated: {0}", DateTime.UtcNow.ToString("u"));
      cb.AppendLine("/// </summary>");
      cb.AppendFormatLine("public static IServiceCollection Add{0}Messaging(this IServiceCollection services)",
        targetDisplayName);

      cb.AppendLine("{");

      using (cb.StartTabulationToRight())
      {
        GenerateHandlerRegistrations(context, cb);
        cb.AppendLine();

        GenerateProducerRegistrations(context, cb);
        cb.AppendLine();

        cb.AppendLine("return services;");
      }

      cb.AppendLine("}");
    }

    cb.AppendLine("}");

    var sourceTextContent = cb.ToString();

    var sourceText = SourceText.From(sourceTextContent, Encoding.UTF8);
    var fileName = $"Messaging.{targetDisplayName}.g.cs";
    context.AddSource(fileName, sourceText);
  }

  private static void GenerateProducerRegistrations(GeneratorExecutionContext context, SourceStringBuilder cb)
  {
    cb.AppendLine("// No Producers Registrations - Not Implemented Yet");

    return;

    if (context.SyntaxContextReceiver is not MessageProducerClassesReceiver receiver)
    {
      context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoProducerClassReceiver, Location.None,
        "some path"));

      return;
    }

    var producerClasses = receiver.Classes;
    if (producerClasses is not { Count: > 0, })
    {
      context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoProducerHandlerClassFound, Location.None,
        "some path"));

      cb.AppendLine("// No Producers Registrations //");

      return;
    }

    cb.AppendLine("/////////////////////////////");
    cb.AppendLine("// Producers Registrations //");
    cb.AppendLine("/////////////////////////////");
  }

  private static void GenerateHandlerRegistrations(GeneratorExecutionContext context, SourceStringBuilder cb)
  {
    if (context.SyntaxContextReceiver is not MessageHandlerClassesReceiver receiver)
    {
      context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoHandlerClassReceiver, Location.None,
        "some path"));

      return;
    }

    var handlerClasses = receiver.Classes;
    if (handlerClasses is not { Count: > 0, })
    {
      context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoHandlerClassFound, Location.None,
        "some path"));

      cb.AppendLine("// No Handlers Registrations //");

      return;
    }

    cb.AppendLine("////////////////////////////");
    cb.AppendLine("// Handlers Registrations //");
    cb.AppendLine("////////////////////////////");

    foreach (var currentClass in handlerClasses)
    {
      var handlerInterface = currentClass.Interfaces
        .Where(i => i != null)
        .FirstOrDefault(i => i.Name.StartsWith("IMessageHandler"));

      var handlerClassName = currentClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
      if (handlerInterface == default)
      {
        cb.AppendFormatLine("// No interface int class '{0}'", handlerClassName);

        continue;
      }

      var keyTypeArg = handlerInterface.TypeArguments[0];
      var valueTypeArg = handlerInterface.TypeArguments[1];

      var keyTypeName = keyTypeArg.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
      var messageTypeName = valueTypeArg.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
      var configName = valueTypeArg.Name.TrimEnd("Message");
      cb.AppendFormatLine("services.AddKafkaJsonConsumer<{0}, {1}>(\"{2}\")", keyTypeName, messageTypeName,
        configName);

      using (cb.StartTabulationToRight())
      {
        var handlerType = currentClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        cb.AppendFormatLine(".AddHandler<{0}>();", handlerType);
      }

      cb.AppendLine();
    }

    // Note [2023-12-26 klappo] disabled for a while
    // WriteDebugInfo(cb, handlerClasses);
  }

  private static void WriteDebugInfo(SourceStringBuilder cb, List<INamedTypeSymbol> namedTypes)
  {
    foreach (var namedType in namedTypes)
    {
      cb.AppendLine("/*");
      cb.AppendLine("Class general info:");
      using (cb.StartTabulationToRight())
      {
        cb.AppendLine(namedType.Name);
        cb.AppendLine(namedType.ToDisplayString());
        cb.AppendLine(namedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        cb.AppendLine("in namespace: " +
                      namedType.ContainingNamespace.ToDisplayString(SymbolDisplayFormat
                        .CSharpErrorMessageFormat));
      }

      cb.AppendLine("Base Type:");
      using (cb.StartTabulationToRight())
      {
        cb.AppendLine(namedType.BaseType?.ToDisplayString());
        cb.AppendLine("kind: " + namedType.BaseType?.Kind);
      }

      cb.AppendLine("Interfaces:");
      using (cb.StartTabulationToRight())
      {
        foreach (var iInterface in namedType.Interfaces
                   .Append(namedType.BaseType)
                   .Where(i => i != null)
                )
        {
          cb.AppendLine(iInterface.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
          cb.AppendLine(iInterface.Name);
          cb.AppendLine(iInterface.Kind.ToString());
          cb.AppendLine(
            "TypeArguments: " +
            string.Join(";",
              iInterface.TypeArguments.Select(t =>
                t.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))));

          cb.AppendLine();
        }
      }

      var attributes = namedType.GetAttributes();
      cb.AppendLine("Attributes:");
      using (cb.StartTabulationToRight())
      {
        foreach (var attribute in attributes)
        {
          var attrType = attribute.AttributeClass.ToDisplayString();
          var s = string.Join(";", attribute.ConstructorArguments);

          cb.AppendLine($"{attrType} : ({s})");
        }
      }

      cb.AppendLine("*/");
      cb.AppendLine();
    }
  }
}
