using System.Runtime.Serialization;

namespace CatalogService.Web.RestModels;

[DataContract(Name = "Link", Namespace = "")]
public sealed class ResourceLink(string href, string rel, string method)
{
    /// <summary>
    /// Defines a URI that can be used to execute this action
    /// </summary>
    [DataMember(Name = "href", Order = 1)]
    public string Href { get; set; } = href;

    /// <summary>
    /// Defines the HTTP verb (method) to use when executing this action
    /// </summary>
    [DataMember(Name = "method", Order = 2)]
    public string Method { get; set; } = method;

    /// <summary>
    /// Identifies the type of action
    /// </summary>
    [DataMember(Name = "rel", Order = 3)]
    public string Rel { get; set; } = rel;
}
