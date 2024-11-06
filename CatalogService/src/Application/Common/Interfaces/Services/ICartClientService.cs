namespace CatalogService.Application.Common.Interfaces.Services;

public interface ICartClientService
{
    Task SendMessageToQueue(string message, CancellationToken cancellationToken);
    Task ReceiveMessageFromQueue(CancellationToken cancellationToken);
}
