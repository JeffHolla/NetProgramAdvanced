﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CartService.Common.Messaging;

public class RabbitQueue : IQueueClient, IDisposable
{
    public string HostName { get; set; }
    public string QueueName { get; set; }

    private IConnection _connection;
    private IChannel _channel;

    public async Task SendMessageAsync(string message, CancellationToken cancellationToken = default)
    {
        await InitializeClientAsync(cancellationToken);

        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync(exchange: string.Empty,
                                         routingKey: QueueName,
                                         mandatory: false,
                                         body: body,
                                         cancellationToken: cancellationToken);
    }

    public async Task<string> ReceiveMessageAsync(CancellationToken cancellationToken = default)
    {
        await InitializeClientAsync(cancellationToken);

        var result = await _channel.BasicGetAsync(QueueName, false, cancellationToken);
        var message = Encoding.UTF8.GetString(result.Body.Span);
        return message;
    }

    public async Task ConfigureReceiveMessageAsync(AsyncEventHandler<BasicDeliverEventArgs> eventMessageHandler, CancellationToken cancellationToken = default)
    {
        await InitializeClientAsync(cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += eventMessageHandler; // Potential memory leak?

        await _channel.BasicConsumeAsync(queue: QueueName,
                                         autoAck: true,
                                         consumer: consumer,
                                         cancellationToken: cancellationToken);
    }

    // TODO: To think about how we can better move this out of here
    // And pass settings
    private async Task InitializeClientAsync(CancellationToken cancellationToken)
    {
        // TODO: Make a check for a created queue
        if (_connection is not null && _channel is not null)
        {
            return;
        }

        // TODO: Move it to the ServiceProvider
        var factory = new ConnectionFactory { HostName = HostName };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        _ = await _channel.QueueDeclareAsync(queue: QueueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
