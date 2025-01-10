using RabbitMQ.Client.Events;

namespace CartService.Common.Messaging;

public interface IQueueClient
{
    string HostName { get; set; }
    string QueueName { get; set; }

    Task SendMessageAsync(string message, CancellationToken cancellationToken = default);
    Task<string> ReceiveMessageAsync(CancellationToken cancellationToken = default);
    Task ConfigureReceiveMessageAsync(AsyncEventHandler<BasicDeliverEventArgs> eventMessageHandler, CancellationToken cancellationToken = default);
}
