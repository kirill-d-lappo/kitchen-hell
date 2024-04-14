using System.ComponentModel.DataAnnotations;

namespace KitchenHell.Messaging.Producers;

public record MessageProducerOptions
{
  [Required]
  public string TopicName { get; set; }

  public bool IsEnabled { get; set; } = false;
}
