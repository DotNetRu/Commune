using System;

namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// One session (time slot) at the meetup
    /// </summary>
    /// <param name="Talk">Talk, the happened at this session</param>
    /// <param name="StartTime">Session starts at this time (with offset from UTC)</param>
    /// <param name="EndTime">Session ends at this time (with offset from UTC)</param>
    public record MeetupSession(Talk Talk,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime);
}
