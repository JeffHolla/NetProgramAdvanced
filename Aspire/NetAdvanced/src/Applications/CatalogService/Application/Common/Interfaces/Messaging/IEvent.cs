namespace CatalogService.Application.Common.Interfaces.Messaging;

public interface IEvent
{
    string Type { get; }
    string Version { get; }
}
