namespace KitchenHell.Messaging.Kafka;

internal static class PollyHelper
{
    private static readonly TimeSpan[] FibonacciSequence =
    {
        TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(8), TimeSpan.FromSeconds(13),
        TimeSpan.FromSeconds(21), TimeSpan.FromSeconds(34), TimeSpan.FromSeconds(55),
    };

    public static Func<int, TimeSpan> FibonacciDelay { get; } =
        retryAttempt =>
        {
            var index = Math.Min(retryAttempt, FibonacciSequence.Length);
            var delay = FibonacciSequence[index - 1];

            return delay;
        };
}