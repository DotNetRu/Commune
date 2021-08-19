namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Venue entity
    /// </summary>
    /// <remarks>
    /// Venue is a place, where meetups can occur
    /// </remarks>
    /// <param name="Id">Venue's identifier</param>
    /// <param name="Name">Venue's name</param>
    /// <param name="Capacity">Venue's capacity (if known)</param>
    /// <param name="Address">Venue's address</param>
    /// <param name="MapUrl">Short url for mapping service with Venue's location</param>
    public record Venue(string? Id, string? Name, int? Capacity, string? Address, string? MapUrl);
}
