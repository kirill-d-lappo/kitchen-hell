using Microsoft.CodeAnalysis;

namespace KitchenHell.Messaging.Kafka.Generators;

internal static class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor NoHandlerClassReceiver = new("MG010",
        "NoHandlerClassReceiver",
        "NoHandlerClassReceiver",
        nameof(MessagingRegistrationsSourceGenerator),
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor NoHandlerClassFound = new("MG0011",
        "NoHandlerClassFound",
        "NoHandlerClassFound",
        nameof(MessagingRegistrationsSourceGenerator),
        DiagnosticSeverity.Info,
        true);

    public static readonly DiagnosticDescriptor NoProducerClassReceiver = new("MG020",
        "NoProducerClassReceiver",
        "NoProducerClassReceiver",
        nameof(MessagingRegistrationsSourceGenerator),
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor NoProducerHandlerClassFound = new("MG0021",
        "NoProducerHandlerClassFound",
        "NoProducerHandlerClassFound",
        nameof(MessagingRegistrationsSourceGenerator),
        DiagnosticSeverity.Info,
        true);
}
