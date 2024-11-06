namespace CartService.Common.Messaging;

public interface IEvent
{
    string Type { get; }
    string Version { get; }
}
