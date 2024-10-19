using System.Collections;
using System.Runtime.Serialization;

namespace CatalogService.Web.RestModels;

[CollectionDataContract(Name = "Links", Namespace = "")]
public sealed class ResourceLinkCollection : IEnumerable<ResourceLink>
{
    private readonly List<ResourceLink> _links = [];

    public ResourceLinkCollection() {}

    public ResourceLinkCollection(IEnumerable<ResourceLink> collection)
    {
        _links = new List<ResourceLink>(collection);
    }

    public void Add(ResourceLink item)
    {
        _links.Add(item);
    }

    public IEnumerator<ResourceLink> GetEnumerator() => _links.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_links).GetEnumerator();
}
