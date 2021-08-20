using System.Collections.Generic;

namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Meetup entity
    /// </summary>
    /// <param name="Id">Meetup identifier</param>
    /// <param name="Name">Meetup name</param>
    /// <param name="Community">Community, that organized this meetup</param>
    /// <param name="Friends">Friends, who helped with te meetup</param>
    /// <param name="Venue">Venue, where meetup took place</param>
    /// <param name="Sessions">Sessions of the meetup</param>
    public record Meetup(string? Id,
        string? Name,
        Community Community,
        IReadOnlyList<Friend> Friends,
        Venue? Venue,
        IReadOnlyList<MeetupSession> Sessions);
}
