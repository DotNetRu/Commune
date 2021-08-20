namespace DotNetRu.Commune.BizLayer.Model
{
    /// <summary>
    /// Community bizlayer model
    /// </summary>
    /// <param name="Id">Community indentifier</param>
    /// <param name="Name">Community name</param>
    /// <param name="City">Community city</param>
    /// <param name="TimeZone">Time zone of community</param>
    /// <param name="VkUrl">Link to vkontakte community's page</param>
    /// <param name="TwitterUrl">Link to community's twitter</param>
    /// <param name="TelegramChannelUrl">Link to community's telegram channel</param>
    /// <param name="TelegramChatUrl">Link to community's telegram chat</param>
    /// <param name="MeetupComUrl">Link to community page at Meetup.com</param>
    /// <param name="TimePadUrl">Link to community's page at TimePad</param>
    public record Community(string Id,
        string Name,
        string City,
        string TimeZone,
        string? VkUrl,
        string? TwitterUrl,
        string? TelegramChannelUrl,
        string? TelegramChatUrl,
        string? MeetupComUrl,
        string? TimePadUrl);
}
