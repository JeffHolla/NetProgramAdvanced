namespace CatalogService.Application.Common.Interfaces.Messaging;

public interface IQueueClient
{
    string HostName { get; set; }
    string QueueName { get; set; }

    Task SendMessageAsync(string message, CancellationToken cancellationToken);
    Task<string> ReceiveMessageAsync(CancellationToken cancellationToken);
}
