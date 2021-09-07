namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Speaker entity
    /// </summary>
    /// <param name="Id">Speaker's identitier</param>
    /// <param name="Name">Speaker's name</param>
    /// <param name="CompanyName">Speaker's company, if present</param>
    /// <param name="CompanyUrl">Speaker's company website, if present</param>
    /// <param name="Description">Speaker's description</param>
    /// <param name="BlogUrl">Speaker's blog url</param>
    /// <param name="ContactsUrl">Speaker's contans page url</param>
    /// <param name="TwitterUrl">Link to speaker's twitter account</param>
    /// <param name="HabrUrl">Link to speaker's account at habr.com</param>
    /// <param name="GitHubUrl">Link to speaker's account at github.com</param>
    public record Speaker(string Id,
        string Name,
        string? CompanyName,
        string? CompanyUrl,
        string Description,
        string? BlogUrl,
        string? ContactsUrl,
        string? TwitterUrl,
        string? HabrUrl,
        string? GitHubUrl);

}
