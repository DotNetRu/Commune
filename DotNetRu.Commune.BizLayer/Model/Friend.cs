namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Friend entity
    /// </summary>
    /// <param name="Id">Friend identifier</param>
    /// <param name="Name">Friend name</param>
    /// <param name="Url">Friend website</param>
    /// <param name="Description">Friend description</param>
    public record Friend(string Id, string Name, string Url, string Description);
}
