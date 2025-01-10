using CatalogService.Application.Common.Interfaces.Messaging;
using CatalogService.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure.Services.CartService;

public class CartClientService : ICartClientService
{
    private readonly IQueueClient _queueClient;

    public CartClientService(IQueueClient queueClient, IOptions<CartQueueOptions> cartOptions)
    {
        _queueClient = queueClient;
        _queueClient.QueueName = cartOptions.Value.QueueName;
        _queueClient.HostName = cartOptions.Value.HostName;
    }

    public async Task SendMessageToQueue(string message, CancellationToken cancellationToken = default)
    {
        await _queueClient.SendMessageAsync(message, cancellationToken);
    }

    public async Task ReceiveMessageFromQueue(CancellationToken cancellationToken = default)
    {
        await _queueClient.ReceiveMessageAsync(cancellationToken);
    }
}
