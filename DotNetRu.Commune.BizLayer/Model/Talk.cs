using System.Collections.Generic;

namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Talk entity
    /// </summary>
    /// <param name="Id">Talk id</param>
    /// <param name="Speakers">List of speakers</param>
    /// <param name="Name">Talk short name</param>
    /// <param name="Description">Talk description</param>
    /// <param name="SeeAlsoTalks">List of connected talks</param>
    /// <param name="CodeUrl">Link to code, discussed at this talk</param>
    /// <param name="SlidesUrl">Link to slides used at the talk</param>
    /// <param name="VideoUrl">Link to video of the talk</param>
    public record Talk(string? Id,
        IReadOnlyList<Speaker> Speakers,
        string? Name,
        string? Description,
        IReadOnlyList<Talk> SeeAlsoTalks,
        string? CodeUrl,
        string? SlidesUrl,
        string? VideoUrl);
}
