using KitchenHell.Business.Messages;
using KitchenHell.Business.Restaurants.Repositories;
using KitchenHell.Messaging.Consumers;

namespace KitchenHell.Business.Restaurants.Messaging.Consumers;

public class OrderCreatedMessageRestaurantHandler : IMessageHandler<string, OrderCreatedMessage>
{
    private readonly IRestaurantRepository _repository;

    public OrderCreatedMessageRestaurantHandler(IRestaurantRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(string key, OrderCreatedMessage value, CancellationToken ct)
    {
        var newOrder = new RestaurantOrderEntity
        {
            OrderId = value.OrderId,
            RestaurantId = value.RestaurantId,
        };

        await _repository.CreateRestaurantOrderAsync(newOrder, ct);

        // Todo [2023-12-20 klappo] need restaurant work imitation: wait some time, then send status update messgaes
    }
}
